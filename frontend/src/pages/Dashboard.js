import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getUserProfile } from '../api';

function Dashboard() {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const res = await getUserProfile();
        setUser(res.data);
      } catch {
        localStorage.removeItem('token');
        navigate('/login');
      }
    };
    fetchProfile();
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/login');
  };

  if (error) return <div className="auth-container"><p>{error}</p></div>;
  if (!user) return <div className="auth-container"><p>Loading...</p></div>;

  return (
    <div className="auth-container">
      <div className="auth-card dashboard-card">
        <h2>Dashboard</h2>
        <div className="welcome-section">
          <p className="welcome-msg">Welcome, <strong>{user.name}</strong>!</p>
          <div className="user-info">
            <p><strong>Email:</strong> {user.email}</p>
            <p><strong>Member since:</strong> {new Date(user.createdAt).toLocaleDateString()}</p>
          </div>
        </div>
        <button className="btn-logout" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </div>
  );
}

export default Dashboard;
