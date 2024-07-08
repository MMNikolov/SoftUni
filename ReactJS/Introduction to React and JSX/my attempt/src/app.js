const rootHTMLElement = document.getElementById('root')

const rootReactElement = ReactDOM.createRoot(rootHTMLElement)

const headingREactElement = React.createElement('h1', null, 'Hello JSX from eaasda')

rootReactElement.render(headingREactElement)