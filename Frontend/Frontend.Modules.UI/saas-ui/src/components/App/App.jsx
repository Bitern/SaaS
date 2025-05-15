import { useState } from 'react'
import reactLogo from '../../assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { SkeletonLoader } from '../SkeletonLoader/SkeletonLoader'

export function App({ isLoading }) {
    const [count, setCount] = useState(0)

    return (
        <>
            <div>
                <SkeletonLoader isLoading={isLoading}>
                    <a href="https://vite.dev" target="_blank">
                        <img src={viteLogo} className="logo" alt="Vite logo" />
                    </a>
                </SkeletonLoader>
                <SkeletonLoader isLoading={isLoading}>
                    <a href="https://react.dev" target="_blank">
                        <img src={reactLogo} className="logo react" alt="React logo" />
                    </a>
                </SkeletonLoader>
            </div>
            <SkeletonLoader isLoading={isLoading}>
                <h1>Vite + React</h1>
            </SkeletonLoader>
            <div className="card">
                <SkeletonLoader isLoading={isLoading}>
                    <button onClick={() => setCount((count) => count + 1)}>
                        count is {count}
                    </button>
                </SkeletonLoader>
                <SkeletonLoader isLoading={isLoading}>
                    <p>
                        Edit <code>src/App.jsx</code> and save to test HMR
                    </p>
                </SkeletonLoader>
                </div>
            <SkeletonLoader isLoading={isLoading}>
                <p className="read-the-docs">
                    Click on the Vite and React logos to learn more
                </p>
            </SkeletonLoader>
        </>
    )
}
