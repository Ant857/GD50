--[[
    GD50
    Breakout Remake

    -- PlayState Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu

    Represents the state of the game in which we are actively playing;
    player should control the paddle, with the ball actively bouncing between
    the bricks, walls, and the paddle. If the ball goes below the paddle, then
    the player should lose one point of health and be taken either to the Game
    Over screen if at 0 health or the Serve screen otherwise.
]]

PlayState = Class{__includes = BaseState}

--[[
    We initialize what's in our PlayState via a state table that we pass between
    states as we go from playing to serving.
]]
function PlayState:enter(params)
    self.paddle = params.paddle
    self.bricks = params.bricks
    self.health = params.health
    self.score = params.score
    self.highScores = params.highScores
    self.balls = { params.ball }
    self.level = params.level
    
    self.isLocked = params.isLocked
    self.key = false
    
    self.powerupSpawnTimer = os.time() + 5
    self.bricksHit = 0

    self.recoverPoints = 5000

    self.powerups = {}

    -- give ball random starting velocity
    self.balls[1].dx = math.random(-200, 200)
    self.balls[1].dy = math.random(-50, -60)
end


function PlayState:update(dt)
    if self.paused then
        if love.keyboard.wasPressed('space') then
            self.paused = false
            gSounds['pause']:play()
        else
            return
        end
    elseif love.keyboard.wasPressed('space') then
        self.paused = true
        gSounds['pause']:play()
        return
    end

    -- update positions based on velocity
    self.paddle:update(dt)
    
    for k, ball in pairs(self.balls) do
      ball:update(dt)
    end
    
    for k, powerup in pairs(self.powerups) do 
      powerup:update(dt)
    end
    
    
    for k, ball in pairs(self.balls) do
      if ball:collides(self.paddle) then
        -- raise ball above paddle in case it goes below it, then reverse dy
        ball.y = self.paddle.y - 8
        ball.dy = -ball.dy

        --
        -- tweak angle of bounce based on where it hits the paddle
        --

        -- if we hit the paddle on its left side while moving left...
        if ball.x < self.paddle.x + (self.paddle.width / 2) and self.paddle.dx < 0 then
          ball.dx = -50 + -(8 * (self.paddle.x + self.paddle.width / 2 - ball.x))
        
        -- else if we hit the paddle on its right side while moving right...
        elseif ball.x > self.paddle.x + (self.paddle.width / 2) and self.paddle.dx > 0 then
          ball.dx = 50 + (8 * math.abs(self.paddle.x + self.paddle.width / 2 - ball.x))
        end
      
        

        gSounds['paddle-hit']:play()
      end
    end
    
    if self.powerupSpawnTimer <= os.time() or self.bricksHit >= 5 then
      local powerup = nil
      powerup = Powerup(math.random(9, 10))
      table.insert(self.powerups, powerup)
      self.powerupSpawnTimer = os.time() + 5
      self.bricksHit = 0
    end

    -- detect collision across all bricks with the ball
    for k, brick in pairs(self.bricks) do
      for b, ball in pairs(self.balls) do
        -- only check collision if we're in play
        if brick.inPlay and ball:collides(brick) then
            
            -- add to score
            if not brick.isLocked then
              self.score = self.score + (brick.tier * 200 + brick.color * 25)
            else
              self.score = self.score + 5000
            end

            -- trigger the brick's hit function, which removes it from play
            brick:hit(self.key)
            self.bricksHit = self.bricksHit + 1

            
            -- if we have enough points, recover a point of health
            if self.score > self.recoverPoints then
                -- can't go above 3 health
                self.health = math.min(3, self.health + 1)

                -- multiply recover points by 2
                self.recoverPoints = math.min(100000, self.recoverPoints * 2)
                self.paddle.size = math.min(self.paddle.size + 1, 4)
                self.paddle.width = self.paddle.size * 32

                -- play recover sound effect
                gSounds['recover']:play()
            end

            -- go to our victory screen if there are no more bricks left
            if self:checkVictory() then
                gSounds['victory']:play()

                gStateMachine:change('victory', {
                    level = self.level,
                    paddle = self.paddle,
                    health = self.health,
                    score = self.score,
                    highScores = self.highScores,
                    ball = self.balls[1],
                    recoverPoints = self.recoverPoints
                })
            end

            --
            -- collision code for bricks
            --
            -- we check to see if the opposite side of our velocity is outside of the brick;
            -- if it is, we trigger a collision on that side. else we're within the X + width of
            -- the brick and should check to see if the top or bottom edge is outside of the brick,
            -- colliding on the top or bottom accordingly 
            --

            -- left edge; only check if we're moving right, and offset the check by a couple of pixels
            -- so that flush corner hits register as Y flips, not X flips
            if ball.x + 2 < brick.x and ball.dx > 0 then
                
                -- flip x velocity and reset position outside of brick
                ball.dx = -ball.dx
                ball.x = brick.x - 8
            
            -- right edge; only check if we're moving left, , and offset the check by a couple of pixels
            -- so that flush corner hits register as Y flips, not X flips
            elseif ball.x + 6 > brick.x + brick.width and ball.dx < 0 then
                
                -- flip x velocity and reset position outside of brick
                ball.dx = -ball.dx
                ball.x = brick.x + 32
            
            -- top edge if no X collisions, always check
            elseif ball.y < brick.y then
                
                -- flip y velocity and reset position outside of brick
                ball.dy = -ball.dy
                ball.y = brick.y - 8
            
            -- bottom edge if no X collisions or top collision, last possibility
            else
                
                -- flip y velocity and reset position outside of brick
                ball.dy = -ball.dy
                ball.y = brick.y + 16
            end

            -- slightly scale the y velocity to speed up the game, capping at +- 150
            if math.abs(ball.dy) < 150 then
                ball.dy = ball.dy * 1.02
            end

            -- only allow colliding with one brick, for corners
            break
        end
      end
    end

    for k, powerup in pairs(self.powerups) do
      if powerup:collides(self.paddle) then
        powerup.inPlay = false
        gSounds['confirm']:play()
        if powerup.skin == 10 then--skin 10 is the key powerup
          self.key = true --if skin is 10 then player has the key
        elseif powerup.skin == 9 then--if it is the additional balls powerup
          for i = 0, 1 do --runs twice to spawn two additional balls
            local newBall = Ball() --spawns new ball
            newBall.skin = math.random(7) --sets the balls skin to a random color
            newBall.x = self.paddle.x + self.paddle.width/2 - newBall.width/2 --sets new balls x location to the middle of the paddle
            newBall.y = self.paddle.y - newBall.height --sets new balls y location so it is on top of paddle
            newBall.dx = math.random(-200, 200) --gives the new ball a random x velocity
            newBall.dy = math.random(-50, -60) --gives new ball a y velocity going upwards
            table.insert(self.balls, newBall) --inserts the ball into the table
          end
        end
      end
    end
    
    for k, ball in pairs(self.balls) do
      if ball.y >= VIRTUAL_HEIGHT then --if the ball hits the bottom of the screen
        table.remove(self.balls, k) --remove the ball from the self.balls table
      end
    end

    for k, powerup in pairs(self.powerups) do
      if powerup.inPlay == false or powerup.y >= VIRTUAL_HEIGHT then --if the powerup is not in play or has gone off the bottom of the screen
        table.remove(self.powerups, k) --delete the powerup
      end
    end

    -- if ball goes below bounds, revert to serve state and decrease health
    if #self.balls <= 0 then
        self.health = self.health - 1
        gSounds['hurt']:play()

        if self.health == 0 then
            gStateMachine:change('game-over', {
                score = self.score,
                highScores = self.highScores
            })
        else
            self.paddle.size = math.max(1, self.paddle.size - 1)
            self.paddle.width = self.paddle.size * 32
            
            
            gStateMachine:change('serve', {
                paddle = self.paddle,
                bricks = self.bricks,
                health = self.health,
                score = self.score,
                highScores = self.highScores,
                level = self.level,
                recoverPoints = self.recoverPoints
            })
        end
    end

    -- for rendering particle systems
    for k, brick in pairs(self.bricks) do
        brick:update(dt)
    end

    if love.keyboard.wasPressed('escape') then
        love.event.quit()
    end
end

function PlayState:render()
    
    --display key powerup icon in bottom left to show if the player has the key
    if self.key == true then
      love.graphics.draw(gTextures['main'], gFrames['powerups'][10], 410, 221) 
    end
    
    -- render bricks
    for k, brick in pairs(self.bricks) do
        brick:render()
    end

    -- render all particle systems
    for k, brick in pairs(self.bricks) do
        brick:renderParticles()
    end

    self.paddle:render()
    
    for k, ball in pairs(self.balls) do  
      ball:render()                     --render all balls on screen
    end

    renderScore(self.score)
    renderHealth(self.health)

    -- pause text, if paused
    if self.paused then
        love.graphics.setFont(gFonts['large'])
        love.graphics.printf("PAUSED", 0, VIRTUAL_HEIGHT / 2 - 16, VIRTUAL_WIDTH, 'center')
    end
    
    for k, powerup in pairs(self.powerups) do
      powerup:render()
    end
end

function PlayState:checkVictory()
    for k, brick in pairs(self.bricks) do
        if brick.inPlay then
            return false
        end 
    end

    return true
end