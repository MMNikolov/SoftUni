function solve() {
  const outputElement = document.getElementById('output');
  const textareaElement = document.getElementById('input');
  
  const text = textareaElement.value;

  const result = text
      .split('.')
      .filter(sentence => !!sentence)
      .reduce((result, sentence, i) => {
          const resultIndex = Math.floor(i / 3);
          if (!result[resultIndex]) {
              result[resultIndex] = []
          }

          result[resultIndex].push(sentence.trim());

          return result;
      }, [])
      .map(sentences => `<p>${sentences.join('. ')}.</p>`)
      .join('\n');

  outputElement.innerHTML = result;
}
