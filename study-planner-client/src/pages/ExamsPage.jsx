import { useEffect, useState } from 'react'
import { getSubjects } from '../services/subjectService'
import {
  getExams,
  createExam,
  updateExam,
  deleteExam
} from '../services/examService'

const examTypes = [
  { value: 1, label: 'Kolokvijum' },
  { value: 2, label: 'Ispit' },
  { value: 3, label: 'Projekat' },
  { value: 4, label: 'Domaci' }
]

function ExamsPage() {
  const [exams, setExams] = useState([])
  const [subjects, setSubjects] = useState([])
  const [editingId, setEditingId] = useState(null)
  const [error, setError] = useState('')

  const [formData, setFormData] = useState({
    title: '',
    examDate: '',
    description: '',
    type: 1,
    subjectId: ''
  })

  const loadData = async () => {
    const [examsData, subjectsData] = await Promise.all([
      getExams(),
      getSubjects()
    ])

    setExams(examsData)
    setSubjects(subjectsData)
  }

  useEffect(() => {
    loadData()
  }, [])

  const handleChange = (e) => {
    const { name, value } = e.target

    setFormData({
      ...formData,
      [name]: name === 'type' || name === 'subjectId'
        ? Number(value)
        : value
    })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')

    const payload = {
      ...formData,
      subjectId: Number(formData.subjectId),
      type: Number(formData.type),
      examDate: new Date(formData.examDate).toISOString()
    }

    try {
      if (editingId) {
        await updateExam(editingId, payload)
      } else {
        await createExam(payload)
      }

      resetForm()
      await loadData()
    } catch {
      setError('Failed to save exam.')
    }
  }

  const handleEdit = (exam) => {
    setEditingId(exam.id)

    setFormData({
      title: exam.title,
      examDate: exam.examDate ? exam.examDate.slice(0, 16) : '',
      description: exam.description || '',
      type: exam.type,
      subjectId: exam.subjectId
    })
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this exam?')) {
      return
    }

    try {
      await deleteExam(id)
      await loadData()
    } catch {
      setError('Failed to delete exam.')
    }
  }

  const resetForm = () => {
    setEditingId(null)
    setFormData({
      title: '',
      examDate: '',
      description: '',
      type: 1,
      subjectId: ''
    })
  }

  const getExamTypeLabel = (value) => {
    return examTypes.find(t => t.value === value)?.label || value
  }

  return (
    <div className="container mt-4">
      <h2>Exams</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      <div className="card">
        <div className="card-body">
            <table className="table table-striped">
                <thead>
                <tr>
                    <th>Title</th>
                    <th>Date</th>
                    <th>Type</th>
                    <th>Subject</th>
                    <th style={{ width: '180px' }}>Actions</th>
                </tr>
                </thead>

                <tbody>
                {exams.map(exam => (
                    <tr key={exam.id}>
                    <td>{exam.title}</td>
                    <td>{new Date(exam.examDate).toLocaleString()}</td>
                    <td>{getExamTypeLabel(exam.type)}</td>
                    <td>{exam.subjectName}</td>
                    <td>
                        <button
                        className="btn btn-sm btn-warning me-2"
                        onClick={() => handleEdit(exam)}
                        >
                        Edit
                        </button>

                        <button
                        className="btn btn-sm btn-danger"
                        onClick={() => handleDelete(exam.id)}
                        >
                        Delete
                        </button>
                    </td>
                    </tr>
                ))}

                {exams.length === 0 && (
                    <tr>
                    <td colSpan="5" className="text-center">
                        No exams found.
                    </td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>
      </div>

      <form onSubmit={handleSubmit} className="card card-body mb-4 mt-4">
        <h5>{editingId ? 'Edit exam' : 'Add exam'}</h5>

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
          <label className="form-label">Exam date</label>
          <input
            type="datetime-local"
            name="examDate"
            className="form-control"
            value={formData.examDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Type</label>
          <select
            name="type"
            className="form-select"
            value={formData.type}
            onChange={handleChange}
          >
            {examTypes.map(type => (
              <option key={type.value} value={type.value}>
                {type.label}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Subject</label>
          <select
            name="subjectId"
            className="form-select"
            value={formData.subjectId}
            onChange={handleChange}
            required
          >
            <option value="">Select subject</option>
            {subjects.map(subject => (
              <option key={subject.id} value={subject.id}>
                {subject.name}
              </option>
            ))}
          </select>
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

    </div>
  )
}

export default ExamsPage