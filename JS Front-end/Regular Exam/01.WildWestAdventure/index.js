function solve(input){
    const heroCount = Number(input.shift());
    const team = {}

    for (let i = 0; i < heroCount; i++) {
        const[name, hp, bullets] = input[i].split(' ')
        
        team[name] = {
            hp: Number(hp),
            bullets: Number(bullets)
        }
    }

    let commandLine = input.shift()

    while (commandLine != 'Ride Off Into Sunset') {
        const[command, name, firstArg, secondArg] = commandLine.split(' - ')
        const hero = team[name]

        switch (command) {
            case 'FireShot':
                const target = firstArg

                if (hero.bullets > 0 && hero.bullets <= 6) {
                    console.log(`${name} has successfully hit ${target} and now has ${hero.bullets -= 1} bullets!`);
                    hero.bullets -= 1
                } else if (hero.bullets == 0){
                    console.log(`${name} doesn't have enough bullets to shoot at ${target}!`);
                }
                break;
            case 'TakeHit':
                const damage = Number(firstArg)
                const attacker = secondArg
                hero.hp -= damage

                if (hero.hp > 0 && hero.hp <= 100) {
                    console.log(`${name} took a hit for ${damage} HP from ${attacker} and now has ${hero.hp} HP!`);
                } else if (hero.hp <= 0){
                    console.log(`${name} was gunned down by ${attacker}!`);
                    delete team[name]
                }
                break;
            case 'Reload':
                if (hero.bullets < 6 && hero.bullets >= 0) {
                    console.log(`${name} reloaded ${6 - hero.bullets} bullets!`);
                    hero.bullets = 6
                } else if(hero.bullets == 6){
                    console.log(`${name}'s pistol is fully loaded!`);
                }
                break;
            case 'PatchUp':
                const amountHpRecovered = Number(firstArg)
                hero.hp += amountHpRecovered

                if (hero.hp >= 100) {
                    console.log(`${name} is in full health!`);
                    hero.hp = 100
                } else if (hero.hp > 0 && hero.hp < 100) {
                    console.log(`${name} patched up and recovered ${amountHpRecovered} HP!`);
                }
                break;
            default:
                break
        }

        commandLine = input.shift()
    }

    if (team) {
        for (const hero in team) {
            console.log(`${hero}`);
            console.log(` HP: ${team[hero].hp}`);
            console.log(` Bullets: ${team[hero].bullets}`);
        }
    } else {
        return
    }
}

solve(['2',
    'Gus 100 0',
    'Walt 100 6',
    'TakeHit - Gus - 80 - Bandit',
    'PatchUp - Gus - 20',
    'Ride Off Into Sunset'])
