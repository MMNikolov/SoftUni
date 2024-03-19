function solve(city){
    Object
        .keys(city)
        .forEach(propName => console.log(`${propName} -> ${city[propName]}`));
}

solve({
    name: "Plovdiv",
    area: 389,
    population: 1162358,
    country: "Bulgaria",
    postCode: "4000"
});