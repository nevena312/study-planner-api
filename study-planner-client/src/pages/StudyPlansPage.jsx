import { useEffect, useState } from 'react'
import {
  getStudyPlans,
  createStudyPlan,
  updateStudyPlan,
  deleteStudyPlan
} from '../services/studyPlanService'

function StudyPlansPage() {
  const [studyPlans, setStudyPlans] = useState([])
  const [editingId, setEditingId] = useState(null)
  const [error, setError] = useState('')

  const [formData, setFormData] = useState({
    title: '',
    startDate: '',
    endDate: '',
    description: ''
  })

  const loadStudyPlans = async () => {
    const data = await getStudyPlans()
    setStudyPlans(data)
  }

  useEffect(() => {
    loadStudyPlans()
  }, [])

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    })
  }

  const buildPayload = () => ({
    title: formData.title,
    startDate: new Date(formData.startDate).toISOString(),
    endDate: new Date(formData.endDate).toISOString(),
    description: formData.description
  })

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')

    try {
      const payload = buildPayload()

      if (editingId) {
        await updateStudyPlan(editingId, payload)
      } else {
        await createStudyPlan(payload)
      }

      resetForm()
      await loadStudyPlans()
    } catch {
      setError('Failed to save study plan.')
    }
  }

  const handleEdit = (plan) => {
    setEditingId(plan.id)

    setFormData({
      title: plan.title,
      startDate: plan.startDate ? plan.startDate.slice(0, 16) : '',
      endDate: plan.endDate ? plan.endDate.slice(0, 16) : '',
      description: plan.description || ''
    })
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this study plan?')) {
      return
    }

    try {
      await deleteStudyPlan(id)
      await loadStudyPlans()
    } catch {
      setError('Failed to delete study plan.')
    }
  }

  const resetForm = () => {
    setEditingId(null)
    setFormData({
      title: '',
      startDate: '',
      endDate: '',
      description: ''
    })
  }

  return (
    <div className="container mt-4">
      <h2>Study Plans</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      <form onSubmit={handleSubmit} className="card card-body mb-4">
        <h5>{editingId ? 'Edit study plan' : 'Add study plan'}</h5>

        <div className="mb-3">
          <label className="form-label">Title</label>
          <input
            type="text"
            name="title"
            className="form-control"
            value={formData.title}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Start date</label>
          <input
            type="datetime-local"
            name="startDate"
            className="form-control"
            value={formData.startDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">End date</label>
          <input
            type="datetime-local"
            name="endDate"
            className="form-control"
            value={formData.endDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Description</label>
          <textarea
            name="description"
            className="form-control"
            value={formData.description}
            onChange={handleChange}
          />
        </div>

        <button type="submit" className="btn btn-primary me-2">
          {editingId ? 'Update' : 'Create'}
        </button>

        {editingId && (
          <button type="button" className="btn btn-secondary mt-2" onClick={resetForm}>
            Cancel
          </button>
        )}
      </form>

      <table className="table table-striped">
        <thead>
          <tr>
            <th>Title</th>
            <th>Start</th>
            <th>End</th>
            <th>Tasks</th>
            <th style={{ width: '180px' }}>Actions</th>
          </tr>
        </thead>

        <tbody>
          {studyPlans.map(plan => (
            <tr key={plan.id}>
              <td>{plan.title}</td>
              <td>{new Date(plan.startDate).toLocaleString()}</td>
              <td>{new Date(plan.endDate).toLocaleString()}</td>
              <td>{plan.taskCount}</td>
              <td>
                <button
                  className="btn btn-sm btn-warning me-2"
                  onClick={() => handleEdit(plan)}
                >
                  Edit
                </button>

                <button
                  className="btn btn-sm btn-danger"
                  onClick={() => handleDelete(plan.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}

          {studyPlans.length === 0 && (
            <tr>
              <td colSpan="5" className="text-center">
                No study plans found.
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  )
}

export default StudyPlansPage