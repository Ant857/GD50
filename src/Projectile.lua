--[[
    GD50
    Legend of Zelda

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

Projectile = Class{}

function Projectile:init(projectile, pixelsToProject, direction, dungeon)
    self.projectile = projectile
    self.pixelsToProject = pixelsToProject
    self.direction = direction
    self.dungeon = dungeon
    
    self.initX = projectile.x
    self.initY = projectile.y
    self.complete = false
end

function Projectile:update(dt)
    
    if self.direction == 'left' then
        self.projectile.x = self.projectile.x - (POT_THROW_SPEED * dt)
        
        if (self.initX - self.projectile.x) > self.pixelsToProject then
            self.complete = true
        end
    elseif self.direction == 'right' then
        self.projectile.x = self.projectile.x + (POT_THROW_SPEED * dt)
        
        if (self.projectile.x - self.initX) > self.pixelsToProject then
            self.complete = true
        end
    elseif self.direction == 'up' then
        self.projectile.y = self.projectile.y - (POT_THROW_SPEED * dt)
        
        if (self.initY - self.projectile.y) > self.pixelsToProject then
            self.complete = true
        end
    elseif self.direction == 'down' then
        self.projectile.y = self.projectile.y + (POT_THROW_SPEED * dt)
        
        if (self.projectile.y - self.initY) > self.pixelsToProject then
            self.complete = true
        end
    end
    
    if self.complete == true then
        for k, obj in ipairs(self.dungeon.currentRoom.objects) do
            if obj.type == 'pot' then
                obj.state = 'broken'
                Timer.after(5, function () table.remove(self.dungeon.currentRoom.objects, k) end)
                --table.remove(self.dungeon.currentRoom.objects, k)
            end
        end
    end
end


function Projectile:render()

end