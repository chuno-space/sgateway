import { useState } from 'react'
import './App.css'
import { Button } from '@/components/ui/button'
import { ThemeProvider } from "@/components/theme-provider"
import { ThemeToggle } from '@/components/theme-toggle'
import { ProfileForm } from '@/components/forms/ProfileForm'
function App() {
  const [count, setCount] = useState(0)

  return (
    <ThemeProvider  defaultTheme="dark" storageKey="sgateway-ui-theme">
      <ThemeToggle/>
      <div className="card">
        <Button onClick={() => setCount((count) => count + 1)}>Click me</Button>
        <p>
        count is {count}
        </p>
        <div className="bg-background text-foreground" >
          Hello
        </div>
      </div>

      <ProfileForm/>
    </ThemeProvider>
  )
}

export default App
