

PlayerPotIdleState = Class{__includes = BaseState}

function PlayerPotIdleState:init(player, dungeon)
    self.player = player
    self.dungeon = dungeon
    
    self.player.offsetY = 0
    self.player.offsetX = 0
    self.player:changeAnimation('idle-pot-' .. self.player.direction)
end

function PlayerPotIdleState:update(dt)
    if love.keyboard.isDown('left') or love.keyboard.isDown('right') or
        love.keyboard.isDown('up') or love.keyboard.isDown('down') then
          self.player:changeState('pot-walk')
    end
    
    if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
        self.player:changeState('pot-throw')
    end
    
    EntityIdleState:update(self, dt)
end

function PlayerPotIdleState:render()
    local anim = self.player.currentAnimation 
    love.graphics.draw(gTextures[anim.texture], gFrames[anim.texture][anim:getCurrentFrame()],
        math.floor(self.player.x - self.player.offsetX), math.floor(self.player.y - self.player.offsetY))
end