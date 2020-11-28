--[[
    GD50
    Pokemon

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

FieldMenuState = Class{__includes = BaseState}

function FieldMenuState:init(battleState)
    self.battleState = battleState
    self.playerPokemon = self.battleState.player.party.pokemon[1]
    
    local stats = self.playerPokemon:statsLevelUp()


    self.HP = self.playerPokemon.HP
    self.attack = self.playerPokemon.attack
    self.defense = self.playerPokemon.defense
    self.speed = self.playerPokemon.speed
    
    self.battleMenu = Menu {
        pointer = false,
        x = VIRTUAL_WIDTH / 2 - 125,
        y = VIRTUAL_HEIGHT / 2 - 75,
        width = 250,
        height = 150,
        items = {
            {
                text = 'HP: ' .. tostring(self.HP - stats[1]) .. ' + ' .. tostring(stats[1]) .. ' = ' .. tostring(self.HP),
            },
            {
                text = 'Attack: ' .. tostring(self.attack - stats[2]) .. ' + ' .. tostring(stats[2]) .. ' = ' .. tostring(self.attack),
            },
            {
                text = 'Defense: ' .. tostring(self.defense - stats[3]) .. ' + ' .. tostring(stats[3]) .. ' = ' .. tostring(self.defense),
            },
            {
                text = 'Speed: ' .. tostring(self.speed - stats[4]) .. ' + ' .. tostring(stats[4]) .. ' = ' .. tostring(self.speed),
            },
        }
    }
end

function FieldMenuState:update(dt)
    self.battleMenu:update(dt)
end

function FieldMenuState:render()
    self.battleMenu:render()
end