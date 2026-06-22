import axiosClient from '../api/axiosClient'

export const getTasks = async () => {
  const response = await axiosClient.get('/studytasks')
  return response.data
}

export const createTask = async (task) => {
  const response = await axiosClient.post('/studytasks', task)
  return response.data
}

export const updateTask = async (id, task) => {
  await axiosClient.put(`/studytasks/${id}`, task)
}

export const deleteTask = async (id) => {
  await axiosClient.delete(`/studytasks/${id}`)
}

export const getTasksByPlan = async (planId) => {
  const response = await axiosClient.get(`/studytasks/by-plan/${planId}`)
  return response.data
}

export const getPendingTasks = async () => {
  const response = await axiosClient.get('/studytasks/pending')
  return response.data
}