import { useEffect, useState } from 'react'
import { getSubjects } from '../services/subjectService'
import { getStudyPlans } from '../services/studyPlanService'
import {
  getTasks,
  createTask,
  updateTask,
  deleteTask
} from '../services/taskService'

const taskStatuses = [
  { value: 1, label: 'Planned' },
  { value: 2, label: 'In Progress' },
  { value: 3, label: 'Completed' }
]

const taskPriorities = [
  { value: 1, label: 'Low' },
  { value: 2, label: 'Medium' },
  { value: 3, label: 'High' }
]

function TasksPage() {
  const [tasks, setTasks] = useState([])
  const [subjects, setSubjects] = useState([])
  const [studyPlans, setStudyPlans] = useState([])
  const [editingId, setEditingId] = useState(null)
  const [error, setError] = useState('')

  const [formData, setFormData] = useState({
    title: '',
    description: '',
    status: 1,
    priority: 2,
    deadline: '',
    estimatedDurationMinutes: 60,
    subjectId: '',
    studyPlanId: ''
  })

  const loadData = async () => {
    const [tasksData, subjectsData, plansData] = await Promise.all([
      getTasks(),
      getSubjects(),
      getStudyPlans()
    ])

    setTasks(tasksData)
    setSubjects(subjectsData)
    setStudyPlans(plansData)
  }

  useEffect(() => {
    loadData()
  }, [])

  const handleChange = (e) => {
    const { name, value } = e.target

    setFormData({
      ...formData,
      [name]:
        name === 'status' ||
        name === 'priority' ||
        name === 'subjectId' ||
        name === 'estimatedDurationMinutes'
          ? Number(value)
          : value
    })
  }

  const buildPayload = () => {
    return {
      title: formData.title,
      description: formData.description,
      status: Number(formData.status),
      priority: Number(formData.priority),
      deadline: formData.deadline
        ? new Date(formData.deadline).toISOString()
        : null,
      estimatedDurationMinutes: Number(formData.estimatedDurationMinutes),
      subjectId: Number(formData.subjectId),
      studyPlanId: formData.studyPlanId ? Number(formData.studyPlanId) : null
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')

    try {
      const payload = buildPayload()

      if (editingId) {
        await updateTask(editingId, payload)
      } else {
        await createTask(payload)
      }

      resetForm()
      await loadData()
    } catch {
      setError('Failed to save task.')
    }
  }

  const handleEdit = (task) => {
    setEditingId(task.id)

    setFormData({
      title: task.title,
      description: task.description || '',
      status: task.status,
      priority: task.priority,
      deadline: task.deadline ? task.deadline.slice(0, 16) : '',
      estimatedDurationMinutes: task.estimatedDurationMinutes,
      subjectId: task.subjectId,
      studyPlanId: task.studyPlanId || ''
    })
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this task?')) {
      return
    }

    try {
      await deleteTask(id)
      await loadData()
    } catch {
      setError('Failed to delete task.')
    }
  }

  const resetForm = () => {
    setEditingId(null)
    setFormData({
      title: '',
      description: '',
      status: 1,
      priority: 2,
      deadline: '',
      estimatedDurationMinutes: 60,
      subjectId: '',
      studyPlanId: ''
    })
  }

  const getStatusLabel = (value) => {
    return taskStatuses.find(s => s.value === value)?.label || value
  }

  const getPriorityLabel = (value) => {
    return taskPriorities.find(p => p.value === value)?.label || value
  }

  return (
    <div className="container mt-4">
      <h2>Tasks</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      <form onSubmit={handleSubmit} className="card card-body mb-4">
        <h5>{editingId ? 'Edit task' : 'Add task'}</h5>

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
          <label className="form-label">Study plan</label>
          <select
            name="studyPlanId"
            className="form-select"
            value={formData.studyPlanId}
            onChange={handleChange}
          >
            <option value="">No study plan</option>
            {studyPlans.map(plan => (
              <option key={plan.id} value={plan.id}>
                {plan.title}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Status</label>
          <select
            name="status"
            className="form-select"
            value={formData.status}
            onChange={handleChange}
          >
            {taskStatuses.map(status => (
              <option key={status.value} value={status.value}>
                {status.label}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Priority</label>
          <select
            name="priority"
            className="form-select"
            value={formData.priority}
            onChange={handleChange}
          >
            {taskPriorities.map(priority => (
              <option key={priority.value} value={priority.value}>
                {priority.label}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Deadline</label>
          <input
            type="datetime-local"
            name="deadline"
            className="form-control"
            value={formData.deadline}
            onChange={handleChange}
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Estimated duration minutes</label>
          <input
            type="number"
            name="estimatedDurationMinutes"
            className="form-control"
            value={formData.estimatedDurationMinutes}
            onChange={handleChange}
            min="0"
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
            <button type="button" className="btn btn-secondary" onClick={resetForm}>
              Cancel
            </button>
          )}
        </div>
      </form>

      <table className="table table-striped">
        <thead>
          <tr>
            <th>Title</th>
            <th>Subject</th>
            <th>Plan</th>
            <th>Status</th>
            <th>Priority</th>
            <th>Deadline</th>
            <th style={{ width: '180px' }}>Actions</th>
          </tr>
        </thead>

        <tbody>
          {tasks.map(task => (
            <tr key={task.id}>
              <td>{task.title}</td>
              <td>{task.subjectName}</td>
              <td>{task.studyPlanTitle || '-'}</td>
              <td>{getStatusLabel(task.status)}</td>
              <td>{getPriorityLabel(task.priority)}</td>
              <td>{task.deadline ? new Date(task.deadline).toLocaleString() : '-'}</td>
              <td>
                <button
                  className="btn btn-sm btn-warning me-2"
                  onClick={() => handleEdit(task)}
                >
                  Edit
                </button>

                <button
                  className="btn btn-sm btn-danger"
                  onClick={() => handleDelete(task.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}

          {tasks.length === 0 && (
            <tr>
              <td colSpan="7" className="text-center">
                No tasks found.
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  )
}

export default TasksPage