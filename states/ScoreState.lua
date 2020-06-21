--[[
    ScoreState Class
    Author: Colton Ogden
    cogden@cs50.harvard.edu

    A simple state used to display the player's score before they
    transition back into the play state. Transitioned to from the
    PlayState when they collide with a Pipe.
]]

ScoreState = Class{__includes = BaseState}

local BRONZE_MEDAL = love.graphics.newImage('bronze_medal.png')
local SILVER_MEDAL = love.graphics.newImage('silver_medal.png')
local GOLD_MEDAL = love.graphics.newImage('gold_medal.png')


--[[
    When we enter the score state, we expect to receive the score
    from the play state so we know what to render to the State.
]]
function ScoreState:enter(params)
    self.score = params.score
end

function ScoreState:update(dt)
    -- go back to play if enter is pressed
    if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
        gStateMachine:change('countdown')
    end
end

function ScoreState:render()
    -- simply render the score to the middle of the screen
    love.graphics.setFont(flappyFont)
    love.graphics.printf('Oof! You lost!', 0, 64, VIRTUAL_WIDTH, 'center')

    love.graphics.setFont(mediumFont)
    love.graphics.printf('Score: ' .. tostring(self.score), 0, 100, VIRTUAL_WIDTH, 'center')

    love.graphics.printf('Press Enter to Play Again!', 0, 160, VIRTUAL_WIDTH, 'center')
    
    if self.score >= 2 and self.score <= 4 then
      love.graphics.draw(BRONZE_MEDAL, 256, 134, 0, .4, .4, 20.5, 37)
    elseif self.score >= 5 and self.score <= 7 then
      love.graphics.draw(SILVER_MEDAL, 256, 134, 0, .4, .4, 20.5, 37)
    elseif self.score >= 8 then
      love.graphics.draw(GOLD_MEDAL, 256, 134, 0, .4, .4, 20.5, 37)
    end
end