namespace LoansApp.Web.Services
{
    public class AuthStateService
    {
        // Evento que se dispara cuando cambia el estado de autenticación
        public event Action? OnChange;

        private bool _isAuthenticated = false;
        private bool _isAdmin = false;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            private set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyStateChanged();
                }
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            private set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    NotifyStateChanged();
                }
            }
        }

        public void SetAuthState(bool isAuthenticated, bool isAdmin)
        {
            IsAuthenticated = isAuthenticated;
            IsAdmin = isAdmin;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
