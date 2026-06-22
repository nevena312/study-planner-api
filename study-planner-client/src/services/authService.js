import axiosClient from '../api/axiosClient'

export const register = async (data) => {
  const response = await axiosClient.post('/auth/register', data)
  return response.data
}

export const login = async (data) => {
  const response = await axiosClient.post('/auth/login', data)
  return response.data
}

export const logout = () => {
  localStorage.removeItem('token')
  localStorage.removeItem('userId')
  localStorage.removeItem('email')
}