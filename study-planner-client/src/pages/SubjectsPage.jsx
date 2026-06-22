import { useEffect, useState } from 'react'
import {
  getSubjects,
  createSubject,
  updateSubject,
  deleteSubject
} from '../services/subjectService'

function SubjectsPage() {
  const [subjects, setSubjects] = useState([])
  const [formData, setFormData] = useState({
    name: '',
    description: ''
  })

  const [editingId, setEditingId] = useState(null)
  const [error, setError] = useState('')

  const loadSubjects = async () => {
    const data = await getSubjects()
    setSubjects(data)
  }

  useEffect(() => {
    loadSubjects()
  }, [])

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')

    try {
      if (editingId) {
        await updateSubject(editingId, formData)
      } else {
        await createSubject(formData)
      }

      setFormData({
        name: '',
        description: ''
      })

      setEditingId(null)
      await loadSubjects()
    } catch {
      setError('Failed to save subject.')
    }
  }

  const handleEdit = (subject) => {
    setEditingId(subject.id)
    setFormData({
      name: subject.name,
      description: subject.description || ''
    })
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this subject?')) {
      return
    }

    try {
      await deleteSubject(id)
      await loadSubjects()
    } catch {
      setError('Failed to delete subject.')
    }
  }

  const handleCancel = () => {
    setEditingId(null)
    setFormData({
      name: '',
      description: ''
    })
  }

  return (
    <div className="container mt-4">
      <h2>Subjects</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      <div className="card">
        <div className="card-body">
          <table className="table table-striped">
            <thead>
              <tr>
                <th>Name</th>
                <th>Description</th>
                <th style={{ width: '180px' }}>Actions</th>
              </tr>
            </thead>

            <tbody>
              {subjects.map(subject => (
                <tr key={subject.id}>
                  <td>{subject.name}</td>
                  <td>{subject.description}</td>
                  <td>
                    <button
                      className="btn btn-sm btn-warning me-2"
                      onClick={() => handleEdit(subject)}
                    >
                      Edit
                    </button>

                    <button
                      className="btn btn-sm btn-danger"
                      onClick={() => handleDelete(subject.id)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}

              {subjects.length === 0 && (
                <tr>
                  <td colSpan="3" className="text-center">
                    No subjects found.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>

      <form onSubmit={handleSubmit} className="card card-body mb-4 mt-4">
        <h5>{editingId ? 'Edit subject' : 'Add subject'}</h5>

        <div className="mb-3">
          <label className="form-label">Name</label>
          <input
            type="text"
            name="name"
            className="form-control"
            value={formData.name}
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

        <div>
          <button type="submit" className="btn btn-primary me-2">
            {editingId ? 'Update' : 'Create'}
          </button>

          {editingId && (
            <button type="button" className="btn btn-secondary" onClick={handleCancel}>
              Cancel
            </button>
          )}
        </div>
      </form>
            
    </div>
  )
}

export default SubjectsPage 