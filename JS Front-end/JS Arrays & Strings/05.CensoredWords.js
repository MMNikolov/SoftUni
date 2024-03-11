function solve(text, word) {
    let index = text.indexOf(word);

    while (index >= 0) {
        text = text.replace(word, '*'.repeat(word.length));

        index = text.indexOf(word);
    }

    console.log(text);
}

function solve2(text, word) {
    // const pattern = new RegExp(word, 'g');
    // const result = text.replace(pattern, '*'.repeat(word.length));
    const result = text.replaceAll(word, '*'.repeat(word.length));

    console.log(result);
}

solve2('A small sentence with some words', 'small')
