import axiosClient from '../api/axiosClient'

export const getStudyPlans = async () => {
  const response = await axiosClient.get('/studyplans')
  return response.data
}

export const createStudyPlan = async (studyPlan) => {
  const response = await axiosClient.post('/studyplans', studyPlan)
  return response.data
}

export const updateStudyPlan = async (id, studyPlan) => {
  await axiosClient.put(`/studyplans/${id}`, studyPlan)
}

export const deleteStudyPlan = async (id) => {
  await axiosClient.delete(`/studyplans/${id}`)
}