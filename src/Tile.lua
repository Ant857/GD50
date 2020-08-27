--[[
    GD50
    Match-3 Remake

    -- Tile Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu

    The individual tiles that make up our game board. Each Tile can have a
    color and a variety, with the varietes adding extra points to the matches.
]]

Tile = Class{}

function Tile:init(x, y, color, variety)
    
    -- board positions
    self.gridX = x
    self.gridY = y

    -- coordinate positions
    self.x = (self.gridX - 1) * 32
    self.y = (self.gridY - 1) * 32

    -- tile appearance/points
    self.color = color
    self.variety = variety
    self.shiny = (math.random(20) == 1)
    
    self.psystem = love.graphics.newParticleSystem(gTextures['particle'], 32)
    self.psystem:setParticleLifetime(1, 5)
    self.psystem:setAreaSpread('normal', 5, 5)
    self.psystem:setColors(255, 191, 0, 100, 255, 191, 0, 0)
    
    if self.shiny then
        self.psystem = love.graphics.newParticleSystem(gTextures['particle'], 3)
        self.psystem:setColors(255, 255, 255, 0, 255, 255, 255, 255, 255, 255, 255, 0)
        self.psystem:setEmitterLifetime(-1)
        self.psystem:setEmissionRate(1)
        self.psystem:setParticleLifetime(3, 5)
        self.psystem:setAreaSpread('normal', 5, 5)
        self.psystem:start()
    end
end

function Tile:update(dt)
    if self.psystem then
        self.psystem:update(dt)
    end
end

function Tile:render(x, y)
    
    -- draw shadow
    love.graphics.setColor(34, 32, 52, 255)
    love.graphics.draw(gTextures['main'], gFrames['tiles'][self.color][self.variety],
        self.x + x + 2, self.y + y + 2)

    -- draw tile itself
    love.graphics.setColor(255, 255, 255, 255)
    love.graphics.draw(gTextures['main'], gFrames['tiles'][self.color][self.variety],
        self.x + x, self.y + y)
      
    if self.shiny then
        love.graphics.draw(self.psystem, self.x + x + 16, self.y + y + 16)
    end
end