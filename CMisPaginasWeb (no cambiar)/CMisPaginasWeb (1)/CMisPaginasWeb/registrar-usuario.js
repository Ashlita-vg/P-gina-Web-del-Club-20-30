// ✅ Dropdown funcional
const btn = document.getElementById('menu-btn');
const content = btn.nextElementSibling;

btn.addEventListener('click', e => {
    e.stopPropagation();
    const isShown = content.classList.toggle('show');
    btn.setAttribute('aria-expanded', isShown);
});

window.addEventListener('click', e => {
    if (!btn.contains(e.target) && !content.contains(e.target)) {
        content.classList.remove('show');
        btn.setAttribute('aria-expanded', false);
    }
});

content.querySelectorAll('a').forEach(link => {
    link.addEventListener('click', () => {
        content.classList.remove('show');
        btn.setAttribute('aria-expanded', false);
    });
});

// ✅ Toggle mostrar/ocultar contraseña
function setupTogglePassword(inputId, toggleId) {
    const passwordInput = document.getElementById(inputId);
    const toggleBtn = document.getElementById(toggleId);

    toggleBtn.addEventListener('click', () => {
        const isPassword = passwordInput.type === 'password';
        passwordInput.type = isPassword ? 'text' : 'password';
        toggleBtn.innerHTML = isPassword ? '<strong>👁️</strong>' : '<strong>🙈</strong>';
    });
}

setupTogglePassword('contrasena', 'toggleContrasena');
setupTogglePassword('confirmar', 'toggleConfirmar');

// ✅ Validación de contraseña
const passwordInput = document.getElementById('contrasena');
const passwordStrength = document.getElementById('password-strength');

passwordInput.addEventListener('input', () => {
    const val = passwordInput.value;

    const hasUpper = /[A-Z]/.test(val);
    const hasLower = /[a-z]/.test(val);
    const hasNumber = /\d/.test(val);
    const hasSpecial = /[!@#$%^&*(),.?":{}|<>_\-]/.test(val);
    const isLong = val.length >= 8;

    // Contador de fuerza
    let strength = [hasUpper, hasLower, hasNumber, hasSpecial, isLong].filter(Boolean).length;

    if (val.length === 0) {
        passwordStrength.textContent = '';
        return;
    }

    if (strength <= 2) {
        passwordStrength.textContent = '⚠️ Contraseña débil';
        passwordStrength.style.color = '#E2261D';
    } else if (strength === 3 || strength === 4) {
        passwordStrength.textContent = '🟡 Contraseña media';
        passwordStrength.style.color = '#FDDD00';
    } else {
        passwordStrength.textContent = '✅ Contraseña fuerte';
        passwordStrength.style.color = '#28A745';
    }
});
