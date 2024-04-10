function solve(input){
    const racerCount = Number(input.shift());
    const team = {}

    for (let i = 0; i < racerCount; i++) {
        const[name, fuel, place] = input[i].split('|')
        
        team[name] = {
            fuel: Number(fuel),
            place: Number(place)
        }
    }

    let commandLine = input.shift()

    while (commandLine != 'Finish') {
        const[command, name, firstArg, secondArg] = commandLine.split(" - ")
        const racer = team[name]

        switch (command) {
            case 'StopForFuel':
                if (racer.fuel < firstArg) {
                    racer.place = secondArg
                    console.log(`${name} stopped to refuel but lost his position, now he is ${Number(secondArg)}.`);
                } else {
                    console.log(`${name} does not need to stop for fuel!`);
                }
                break;
            case 'Overtaking':
                const firstRiderPosition = team[name].place
                const secondRiderPosition = team[firstArg].place

                if (firstRiderPosition < secondRiderPosition) {
                    team[name].place = secondRiderPosition
                    team[firstArg].place = firstRiderPosition

                    console.log(`${name} overtook ${firstArg}!`);
                }
                break;
            case 'EngineFail':
                console.log(`${name} is out of the race because of a technical issue, ${firstArg} laps before the finish.`);
                delete team[name]
                break;
        }

        commandLine = input.shift()
    }

    for (const racer in team) {
        console.log(`${racer}`);
        console.log(`   Final position: ${team[racer].place}`);
    }
}

solve(["3",

"Valentino Rossi|100|1",

"Marc Marquez|90|2",

"Jorge Lorenzo|80|3",

"StopForFuel - Valentino Rossi - 50 - 1",

"Overtaking - Marc Marquez - Jorge Lorenzo",

"EngineFail - Marc Marquez - 10",

"Finish"])