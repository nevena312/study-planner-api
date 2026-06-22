import { useEffect, useState } from 'react'
import { getDashboard } from '../services/dashboardService'

function DashboardPage() {
  const [dashboard, setDashboard] = useState(null)
  const [error, setError] = useState('')

  useEffect(() => {
    const loadDashboard = async () => {
      try {
        const data = await getDashboard()
        setDashboard(data)
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
          <div className="card">
            <div className="card-body">
              <h5>Subjects</h5>
              <p className="fs-3">{dashboard.subjectCount}</p>
            </div>
          </div>
        </div>

        <div className="col-md-3 mb-3">
          <div className="card">
            <div className="card-body">
              <h5>Exams</h5>
              <p className="fs-3">{dashboard.examCount}</p>
            </div>
          </div>
        </div>

        <div className="col-md-3 mb-3">
          <div className="card">
            <div className="card-body">
              <h5>Pending tasks</h5>
              <p className="fs-3">{dashboard.pendingTaskCount}</p>
            </div>
          </div>
        </div>

        <div className="col-md-3 mb-3">
          <div className="card">
            <div className="card-body">
              <h5>Completed tasks</h5>
              <p className="fs-3">{dashboard.completedTaskCount}</p>
            </div>
          </div>
        </div>
      </div>

      <div className="card mt-4">
        <div className="card-body">
          <h5>Next exam</h5>

          {dashboard.nextExamTitle ? (
            <>
              <p className="mb-1">
                <strong>{dashboard.nextExamTitle}</strong>
              </p>
              <p className="mb-1">{dashboard.nextExamSubjectName}</p>
              <p className="mb-0">
                {new Date(dashboard.nextExamDate).toLocaleString()}
              </p>
            </>
          ) : (
            <p className="mb-0">No upcoming exams.</p>
          )}
        </div>
      </div>
    </div>
  )
}

export default DashboardPage