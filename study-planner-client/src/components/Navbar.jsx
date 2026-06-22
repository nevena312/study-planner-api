import { Link, useNavigate } from 'react-router-dom'
import { logout } from '../services/authService'

function Navbar() {
  const navigate = useNavigate()
  const email = localStorage.getItem('email')

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <Link className="navbar-brand" to="/dashboard">
          Study Planner
        </Link>

        <div className="navbar-nav me-auto">
          <Link className="nav-link" to="/dashboard">Dashboard</Link>
          <Link className="nav-link" to="/subjects">Subjects</Link>
          <Link className="nav-link" to="/exams">Exams</Link>
          <Link className="nav-link" to="/tasks">Tasks</Link>
          <Link className="nav-link" to="/studyplans">Study Plans</Link>
        </div>

        <span className="navbar-text me-3">{email}</span>

        <button className="btn btn-outline-light btn-sm" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </nav>
  )
}

export default Navbar