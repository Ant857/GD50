PlayerPotThrowState = Class{__includes = BaseState}

function PlayerPotThrowState:init(player, dungeon)
    self.player = player
    self.dungeon = dungeon

    -- render offset for spaced character sprite
    self.player.offsetY = 0
    self.player.offsetX = 0
    
    self.player:changeAnimation('pot-throw-' .. self.player.direction)
    
    self.projectile = nil
end

function PlayerPotThrowState:enter(params)
    --sounds
end

function PlayerPotThrowState:update(dt)
    
    if self.player.currentAnimation.timesPlayed > 0 then
        self.player:changeAnimation('idle-' .. self.player.direction)
    end
    
    if self.projectile == nil then
        self.player.carryingPot.projecting = true
        self.projectile = Projectile(self.player.carryingPot, TILE_SIZE*4, self.player.direction, self.dungeon)
    end
    
    self.projectile:update(dt)
    
    if self.projectile.complete then
        self.player.carryingPot.projecting = false
        self.player.carryingPot = nil
        self.player:changeState('idle')
    end
end

function PlayerPotThrowState:render()
    local anim = self.player.currentAnimation 
    love.graphics.draw(gTextures[anim.texture], gFrames[anim.texture][anim:getCurrentFrame()],
        math.floor(self.player.x - self.player.offsetX), math.floor(self.player.y - self.player.offsetY))
end