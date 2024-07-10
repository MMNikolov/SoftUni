import { useState } from "react"

export default function Counter(){
    const [count, setCount] = useState(0)

    const PlusButtonEventHandler = () => {
        setCount(count + 1)
    }

    const ResetButtonEventHandler = () => {
        setCount(0)
    }

    const MinusButtonEventHandler = () => {
        setCount(count - 1)
    }

    
    return (
        <>
            <h2>Counter</h2>

            <p>{count}</p>

            <button onClick={MinusButtonEventHandler}>-</button>
            <button onClick={ResetButtonEventHandler}>reset</button>
            <button onClick={PlusButtonEventHandler}>+</button>
        </>
    )
}