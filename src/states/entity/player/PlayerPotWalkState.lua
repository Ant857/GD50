

PlayerPotWalkState = Class{__includes = BaseState}

function PlayerPotWalkState:init(player, dungeon)
    self.entity = player
    self.dungeon = dungeon
    
    
    self.entity.offsetY = 0
    self.entity.offsetX = 0
    
    local direction = self.entity.direction
    
    self.entity:changeAnimation('pot-walk-' .. self.entity.direction)
end

function PlayerPotWalkState:enter(params)
    self.entity.currentAnimation:refresh()
end

function PlayerPotWalkState:update(dt)
  
    if love.keyboard.isDown('left') then
        self.entity.direction = 'left'
        self.entity:changeAnimation('pot-walk-' .. self.entity.direction)
    elseif love.keyboard.isDown('right') then
        self.entity.direction = 'right'
        self.entity:changeAnimation('pot-walk-' .. self.entity.direction)
    elseif love.keyboard.isDown('down') then
        self.entity.direction = 'down'
        self.entity:changeAnimation('pot-walk-' .. self.entity.direction)
    elseif love.keyboard.isDown('up') then
        self.entity.direction = 'up'
        self.entity:changeAnimation('pot-walk-' .. self.entity.direction)
    else
        self.entity:changeState('pot-idle')
    end
    
    EntityWalkState.update(self, dt)
    
    
    self.entity.carryingPot.x = self.entity.x
    self.entity.carryingPot.y = self.entity.y - 8
    
    if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
        self.entity:changeState('pot-throw')
    end
end

function PlayerPotWalkState:render()
    local anim = self.entity.currentAnimation 
    love.graphics.draw(gTextures[anim.texture], gFrames[anim.texture][anim:getCurrentFrame()],
        math.floor(self.entity.x - self.entity.offsetX), math.floor(self.entity.y - self.entity.offsetY))
end