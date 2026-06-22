import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import RegisterPage from './pages/RegisterPage'
import DashboardPage from './pages/DashboardPage'
import Navbar from './components/Navbar'
import ProtectedRoute from './components/ProtectedRoute'
import SubjectsPage from './pages/SubjectsPage'
import ExamsPage from './pages/ExamsPage'

function App() {
  const protectedPage = (page) => (
    <ProtectedRoute>
      <Navbar />
      {page}
    </ProtectedRoute>
  )

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        <Route path="/dashboard" element={protectedPage(<DashboardPage />)} />
        <Route path="/subjects" element={protectedPage(<SubjectsPage />)} />
        <Route path="/exams" element={protectedPage(<ExamsPage />)} />
        <Route path="/tasks" element={protectedPage(<h2 className="container mt-4">Tasks page</h2>)} />
        <Route path="/studyplans" element={protectedPage(<h2 className="container mt-4">Study Plans page</h2>)} />
      </Routes>
    </BrowserRouter>
  )
}

export default App