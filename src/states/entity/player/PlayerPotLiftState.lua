

PlayerPotLiftState = Class{__includes = BaseState}

function PlayerPotLiftState:init(player, dungeon)
    self.player = player
    self.dungeon = dungeon
    
    
    self.player.offsetY = 0
    self.player.offsetX = 0
    
    local direction = self.player.direction
    
    self.player:changeAnimation('pot-lift-' .. self.player.direction)
    
    self.nearestPot = nil
end

function PlayerPotLiftState:enter(params)
    gSounds['sword']:stop()
    gSounds['sword']:play()
end

function PlayerPotLiftState:update(dt)
    
    if self.player.currentAnimation.timesPlayed > 0 then
        self.player.currentAnimation.timesPlayed = 0
        
        
        local playerAndNearestPotDifference = nil
        for k, obj in ipairs(self.dungeon.currentRoom.objects) do
            if obj.type == 'pot' then
                

                if self.nearestPot == nil then
                    self.nearestPot = obj
                    playerAndNearestPotDifference = math.abs(self.player.y - obj.y) + math.abs(self.player.x - obj.x)
                else
                    playerAndNearestPotDifference = math.abs(self.player.y - self.nearestPot.y) + math.abs(self.player.x - self.nearestPot.x)
                    if(math.abs(self.player.y - obj.y) + math.abs(self.player.x - obj.x)) < playerAndNearestPotDifference then
                        self.nearestPot = nil
                        playerAndNearestPotDifference = math.abs(self.player.y - obj.y) + math.abs(self.player.x - obj.x)
                    end
                end
            end
        end
        
        
        if playerAndNearestPotDifference < 30 then
            
            self.player.carryingPot = self.nearestPot
            
            
            self.player:changeState('pot-walk')
        else
            self.player:changeState('idle')
        end
    end
end

function PlayerPotLiftState:render()
    local anim = self.player.currentAnimation 
    love.graphics.draw(gTextures[anim.texture], gFrames[anim.texture][anim:getCurrentFrame()],
        math.floor(self.player.x - self.player.offsetX), math.floor(self.player.y - self.player.offsetY))
end