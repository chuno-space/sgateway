import { useState } from 'react'
import './App.css'
import { callGrpcService } from './service'

function App() {
  const [count, setCount] = useState(0)
  const [message, setMessage] = useState("")
  const onBtnClick = async ()=>{
    setCount((count) => count + 1)
    var message = await callGrpcService();
    console.log(message);
    setMessage(message);
  }
  return (
    <>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={onBtnClick}>
          count is {count}
        </button>
        <p>{message}</p>
      </div>
    </>
  )
}

export default App
