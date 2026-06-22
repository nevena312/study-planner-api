import axiosClient from '../api/axiosClient'

export const getSubjects = async () => {
  const response = await axiosClient.get('/subjects')
  return response.data
}

export const createSubject = async (subject) => {
  const response = await axiosClient.post('/subjects', subject)
  return response.data
}

export const updateSubject = async (id, subject) => {
  await axiosClient.put(`/subjects/${id}`, subject)
}

export const deleteSubject = async (id) => {
  await axiosClient.delete(`/subjects/${id}`)
}