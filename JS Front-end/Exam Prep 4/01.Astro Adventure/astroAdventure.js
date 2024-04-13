function solve(input) {
    const astronautCount = Number(input.shift())
    const team = {}

    for (let i = 0; i < astronautCount; i++) {
        const[name, oxygen, energy] = input[i].split(' ')
        
        team[name] = {
            oxygen: Number(oxygen),
            energy: Number(energy)
        }
    }
    
    let commandLine = input.shift()

    while (commandLine != 'End') {
        const[command, name, arg] = commandLine.split(' - ')
        const astronaut = team[name]

        switch (command) {
            case 'Explore':
                const energyNeeded = Number(arg)
                const energyLeft = astronaut.energy - energyNeeded

                if (astronaut.energy > energyNeeded) {
                    console.log(`${name} has successfully explored a new area and now has ${energyLeft} energy!`);
                    astronaut.energy -= energyNeeded
                } else{
                    console.log(`${astronaut} does not have enough energy to explore!`);
                }
                break;
            case 'Refuel':
                const refuelAmount = Number(arg)
                astronaut.energy += refuelAmount
                
                if (astronaut.energy >= 200) {
                    console.log(`${name} refueled their energy by ${200 - (astronaut.energy - refuelAmount)}!`);
                    astronaut.energy = 200
                } else{
                    console.log(`${name} refueled their energy by ${refuelAmount}!`);
                }
                break;
            case 'Breathe':
                const breatheAmount = Number(arg)
                astronaut.oxygen += breatheAmount

                if (astronaut.oxygen >= 100) {
                    console.log(`${name} took a breath and recovered ${100 - (astronaut.oxygen - breatheAmount)} oxygen!`);
                    astronaut.oxygen = 100
                } else{
                    console.log(`${name} took a breath and recovered ${breatheAmount} oxygen!`);
                }
                break;
        }

        commandLine = input.shift()
    }

    for (const astronaut in team) {
        console.log(`Astronaut: ${astronaut}, Oxygen: ${team[astronaut].oxygen}, Energy: ${team[astronaut].energy}`);
    }
}

solve([ '4', 'Alice 60 100', 'Bob 40 80', 'Charlie 70 150', 'Dave 80 180', 'Explore - Bob - 60', 'Refuel - Alice - 30', 'Breathe - Charlie - 50', 'Refuel - Dave - 40', 'Explore - Bob - 40', 'Breathe - Charlie - 30', 'Explore - Alice - 40', 'End'])