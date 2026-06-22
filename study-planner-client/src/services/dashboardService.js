import axiosClient from '../api/axiosClient'

export const getDashboard = async () => {
  const response = await axiosClient.get('/dashboard')
  return response.data
}