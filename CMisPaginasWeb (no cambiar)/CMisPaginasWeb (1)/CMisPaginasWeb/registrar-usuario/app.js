// ✅ URL base de la API
const API_BASE_URL = "https://localhost:7217/Usuario";

// ✅ Dropdown funcional
const btn = document.getElementById('menu-btn');
const content = btn ? btn.nextElementSibling : null;

if (btn && content) {
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
}

// ✅ Toggle mostrar/ocultar contraseña
function setupTogglePassword(inputId, toggleId) {
  const passwordInput = document.getElementById(inputId);
  const togglePassword = document.getElementById(toggleId);

  if (!passwordInput || !togglePassword) return;

  togglePassword.addEventListener('click', () => {
    const isPassword = passwordInput.type === 'password';
    passwordInput.type = isPassword ? 'text' : 'password';
    togglePassword.innerHTML = isPassword
      ? '<strong>Ocultar</strong>'
      : '<strong>Mostrar</strong>';
  });
}

setupTogglePassword('contrasena', 'toggleContrasena');
setupTogglePassword('confirmar', 'toggleConfirmar');

// ✅ Evaluar fuerza de contraseña
const passwordInput = document.getElementById('contrasena');
const passwordStrength = document.getElementById('passwordStrength');

if (passwordInput && passwordStrength) {
  passwordInput.addEventListener('input', () => {
    const val = passwordInput.value;
    let strength = 0;

    if (val.length >= 6) strength++;
    if (/[A-Z]/.test(val)) strength++;
    if (/[a-z]/.test(val)) strength++;
    if (/\d/.test(val)) strength++;
    if (/[!@#$%^&*(),.?":{}|<>]/.test(val)) strength++;

    if (val.length === 0) {
      passwordStrength.textContent = '';
    } else if (strength <= 2) {
      passwordStrength.textContent = 'Contraseña débil';
      passwordStrength.style.color = '#E2261D';
    } else if (strength === 3 || strength === 4) {
      passwordStrength.textContent = 'Contraseña media';
      passwordStrength.style.color = '#FDDD00';
    } else {
      passwordStrength.textContent = 'Contraseña fuerte';
      passwordStrength.style.color = '#1AAB2A';
    }
  });
}

// ✅ Envío del formulario a la API
const form = document.getElementById('registroForm');

if (form) {
  form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const nombreElem = document.getElementById('nombre');
    const apellidosElem = document.getElementById('apellidos');
    const correoElem = document.getElementById('correo');
    const telefonoElem = document.getElementById('telefono');
    const codigoElem = document.getElementById('codigo');
    const contrasenaElem = document.getElementById('contrasena');
    const confirmarElem = document.getElementById('confirmar');

    if (!nombreElem || !apellidosElem || !correoElem || !telefonoElem || !codigoElem || !contrasenaElem || !confirmarElem) {
      alert('Formulario incompleto. Faltan campos.');
      return;
    }

    const nombre = nombreElem.value.trim();
    const apellidos = apellidosElem.value.trim();
    const correo = correoElem.value.trim();
    const telefono = telefonoElem.value.trim();
    const codigo = codigoElem.value;
    const contrasena = contrasenaElem.value;
    const confirmar = confirmarElem.value;

    let valid = true;

    // Validaciones
    const errorNombre = document.getElementById('errorNombre');
    const errorApellidos = document.getElementById('errorApellidos');
    const errorCorreo = document.getElementById('errorCorreo');
    const errorTelefono = document.getElementById('errorTelefono');
    const errorConfirmar = document.getElementById('errorConfirmar');

    if (errorNombre) errorNombre.style.display = !nombre ? 'block' : 'none';
    if (errorApellidos) errorApellidos.style.display = !apellidos ? 'block' : 'none';
    if (errorCorreo) errorCorreo.style.display = !correo.includes('@') ? 'block' : 'none';
    if (errorTelefono) errorTelefono.style.display = !/^\d{4,14}$/.test(telefono) ? 'block' : 'none';
    if (errorConfirmar) errorConfirmar.style.display = contrasena !== confirmar ? 'block' : 'none';

    if (!nombre || !apellidos || !correo.includes('@') || !/^\d{4,14}$/.test(telefono) || contrasena !== confirmar) {
      valid = false;
    }

    if (!valid) return;

    const telefonoCompleto = `${codigo || ''} ${telefono}`.trim();
    const data = { nombre, apellidos, correo, telefono: telefonoCompleto, contrasena };

    try {
      const response = await fetch(API_BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data),
      });

      if (!response.ok) {
        const errorText = await response.text();
        console.error("Error en la solicitud:", errorText);
        throw new Error(errorText);
      }

      alert(`Registro exitoso para ${nombre} ${apellidos}.`);
      form.reset();
      if (passwordStrength) passwordStrength.textContent = '';
    } catch (error) {
      alert('No se pudo registrar. Intenta de nuevo más tarde.');
      console.error('Error detallado:', error);
    }
  });
}
