function solve(input) {
    const words = input.split(' ');
    const wordOccurances = {};

    for (const word of words) {
        if (!wordOccurances.hasOwnProperty(word.toLowerCase())) {
            wordOccurances[word.toLowerCase()] = 0;
        }

        wordOccurances[word.toLowerCase()]++;
    }

    const result = [];
    for (const word in wordOccurances) {
        if (wordOccurances[word] % 2 !== 0) {
            result.push(word);
        }
    }

    console.log(result.join(' '));
}

solve('Java C# Php PHP Java PhP 3 C# 3 1 5 C#')