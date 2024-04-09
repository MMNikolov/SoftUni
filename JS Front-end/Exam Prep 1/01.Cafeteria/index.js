function solve(input){
    const baristaCount = Number(input.shift());
    const team = {};

    for (let i = 0; i < baristaCount; i++) {
        const [name, shift, coffeeTypes] = input[i].split(' ');

        team[name] = {
            shift,
            coffeeTypes: coffeeTypes.split(',')
        }
    }

    let commandLine = input.shift();

    while (commandLine != 'Closed') {
        const [command, name, firstArg, secondArg] = commandLine.split(' / ')
        const barista = team[name]

        switch (command) {
            case 'Prepare':
                if (barista.shift === firstArg && barista.coffeeTypes.includes(secondArg)) {
                    console.log(`${name} has prepared a ${secondArg} for you!`);
                } else{
                    console.log(`${name} is not available to prepare a ${secondArg}.`);
                }
                break;
            case 'Change Shift':
                barista.shift = firstArg
                console.log(`${name} has updated his shift to: ${firstArg}`);
                break;
            case 'Learn':
                if (barista.coffeeTypes.includes(firstArg)) {
                    console.log(`${name} knows how to make ${firstArg}.`);
                } else{
                    barista.coffeeTypes.push(firstArg)
                    console.log(`${name} has learned a new coffee type: ${firstArg}.`);
                }
                break;
        }

        commandLine = input.shift();
    }

    for (const baristaName in team) {
        console.log(`Barista: ${baristaName}, Shift: ${team[baristaName].shift}, Drinks: ${team[baristaName].coffeeTypes.join(', ')}`);
    }
}

solve([
    '3',
    'Alice day Espresso,Cappuccino',
    'Bob night Latte,Mocha',
    'Carol day Americano,Mocha',
    'Prepare / Alice / day / Espresso',
    'Change Shift / Bob / night',
    'Learn / Carol / Latte',
    'Learn / Bob / Latte',
    'Prepare / Bob / night / Latte',
    'Closed'
])