Powerup = Class{}

function Powerup:init(skin) --pass variable type to Powerup class
  self.skin = skin -- set type of powerup
  
  self.width = 16  --set width of powerup
  self.height = 16 -- set height of powerup
  
  self.dy = 30 --set y velocity so powerup is falling
  self.dx = 0
  
  self.x = math.random(20, 412) --set x spawn location of the powerup
  self.y = 50 --set y spawn location of the powerup
  
  self.inPlay = true
end

function Powerup:collides(target)
    -- first, check to see if the left edge of either is farther to the right
    -- than the right edge of the other
    if self.x > target.x + target.width or target.x > self.x + self.width then
        return false
    end

    -- then check to see if the bottom edge of either is higher than the top
    -- edge of the other
    if self.y > target.y + target.height or target.y > self.y + self.height then
        return false
    end 

    -- if the above aren't true, they're overlapping
    return true
end

function Powerup:update(dt)
  self.y = self.y + self.dy * dt --move powerups y position
end

function Powerup:render()
  love.graphics.draw(gTextures['main'], gFrames['powerups'][self.skin], self.x, self.y)
end