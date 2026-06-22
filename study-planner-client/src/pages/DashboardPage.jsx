import { useEffect, useState } from 'react'
import { getDashboard } from '../services/dashboardService'
import { Link } from 'react-router-dom'
import { getUpcomingExams } from '../services/examService'

function DashboardPage() {
  const [dashboard, setDashboard] = useState(null)
  const [error, setError] = useState('')

  const [upcomingExams, setUpcomingExams] = useState([])

  useEffect(() => {
    const loadDashboard = async () => {
      try {
        const data = await getDashboard()
        setDashboard(data)

        const dashboardData = await getDashboard()
        const examsData = await getUpcomingExams()

        setDashboard(dashboardData)
        setUpcomingExams(examsData)
      } catch {
        setError('Failed to load dashboard.')
      }
    }

    loadDashboard()
  }, [])

  if (error) {
    return <div className="container mt-4 alert alert-danger">{error}</div>
  }

  if (!dashboard) {
    return <div className="container mt-4">Loading...</div>
  }

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Dashboard</h2>

      <div className="row">
        <div className="col-md-3 mb-3">
            <Link to="/subjects" className="dashboard-card-link">
            <div className="card dashboard-card">
                <div className="card-body">
                <h5>Subjects</h5>
                <p className="fs-3">{dashboard.subjectCount}</p>
                </div>
            </div>
            </Link>
        </div>

        <div className="col-md-3 mb-3">
            <Link to="/exams" className="dashboard-card-link">
            <div className="card dashboard-card">
                <div className="card-body">
                <h5>Exams</h5>
                <p className="fs-3">{dashboard.examCount}</p>
                </div>
            </div>
            </Link>
        </div>

        <div className="col-md-3 mb-3">
            <Link to="/tasks" className="dashboard-card-link">
            <div className="card dashboard-card">
                <div className="card-body">
                <h5>Pending tasks</h5>
                <p className="fs-3">{dashboard.pendingTaskCount}</p>
                </div>
            </div>
            </Link>
        </div>

        <div className="col-md-3 mb-3">
            <Link to="/tasks" className="dashboard-card-link">
            <div className="card dashboard-card">
                <div className="card-body">
                <h5>Completed tasks</h5>
                <p className="fs-3">{dashboard.completedTaskCount}</p>
                </div>
            </div>
            </Link>
        </div>
        </div>

      <div className="card mt-4 next-exam-card">
        <div className="card-body">
            <h5>Next exam</h5>

            {dashboard.nextExamTitle ? (
            <>
                <p className="next-exam-title mb-1">
                {dashboard.nextExamTitle}
                </p>

                <p className="next-exam-subject mb-1">
                {dashboard.nextExamSubjectName}
                </p>

                <p className="mb-0">
                {new Date(dashboard.nextExamDate).toLocaleString()}
                </p>
            </>
            ) : (
            <p className="mb-0">No upcoming exams.</p>
            )}
        </div>
      </div>

      <div className="card mt-4">
        <div className="card-body">
            <h5>Upcoming exams</h5>

            {upcomingExams.length === 0 ? (
            <p className="mb-0">No upcoming exams.</p>
            ) : (
            <table className="table table-sm table-striped mt-3">
                <thead>
                <tr>
                    <th>Title</th>
                    <th>Subject</th>
                    <th>Date</th>
                </tr>
                </thead>

                <tbody>
                {upcomingExams.map(exam => (
                    <tr key={exam.id}>
                    <td>{exam.title}</td>
                    <td>{exam.subjectName}</td>
                    <td>{new Date(exam.examDate).toLocaleString()}</td>
                    </tr>
                ))}
                </tbody>
            </table>
            )}
        </div>
      </div>
    </div>
  )
}

export default DashboardPage