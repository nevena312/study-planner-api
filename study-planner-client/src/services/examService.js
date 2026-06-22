import axiosClient from '../api/axiosClient'

export const getExams = async () => {
  const response = await axiosClient.get('/exams')
  return response.data
}

export const createExam = async (exam) => {
  const response = await axiosClient.post('/exams', exam)
  return response.data
}

export const updateExam = async (id, exam) => {
  await axiosClient.put(`/exams/${id}`, exam)
}

export const deleteExam = async (id) => {
  await axiosClient.delete(`/exams/${id}`)
}

export const getUpcomingExams = async () => {
  const response = await axiosClient.get('/exams/upcoming')
  return response.data
}