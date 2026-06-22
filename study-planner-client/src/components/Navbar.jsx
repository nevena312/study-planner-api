import { Link, NavLink, useNavigate } from 'react-router-dom'
import { logout } from '../services/authService'

function Navbar() {
  const navigate = useNavigate()
  const email = localStorage.getItem('email')

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <nav className="navbar navbar-expand-lg">
      <div className="container">
        <Link className="navbar-brand" to="/dashboard">
          Study Planner
        </Link>

        <div className="navbar-nav me-auto">
          <NavLink
            to="/dashboard"
            className={({ isActive }) =>
                isActive ? 'nav-link active-nav-link' : 'nav-link'
            }
            >
            Dashboard
          </NavLink>
          <NavLink
            to="/subjects"
            className={({ isActive }) =>
                isActive ? 'nav-link active-nav-link' : 'nav-link'
            }
            >
            Subjects
          </NavLink>
          <NavLink
            to="/exams"
            className={({ isActive }) =>
                isActive ? 'nav-link active-nav-link' : 'nav-link'
            }
            >
            Exams
          </NavLink>
          <NavLink
            to="/tasks"
            className={({ isActive }) =>
                isActive ? 'nav-link active-nav-link' : 'nav-link'
            }
            >
            Tasks
          </NavLink>
          <NavLink
            to="/studyplans"
            className={({ isActive }) =>
                isActive ? 'nav-link active-nav-link' : 'nav-link'
            }
            >
            Study Plans
          </NavLink>
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