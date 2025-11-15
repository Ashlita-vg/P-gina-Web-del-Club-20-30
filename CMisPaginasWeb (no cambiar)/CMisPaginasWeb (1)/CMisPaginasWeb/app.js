// ✅ URL base de la API
const API_BASE_URL = "https://localhost:7217/Usuario";

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
  const togglePassword = document.getElementById(toggleId);

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

// ✅ Envío del formulario a la API
const form = document.getElementById('registroForm');

form.addEventListener('submit', async function (e) {
  e.preventDefault();

  const nombre = document.getElementById('nombre').value.trim();
  const apellidos = document.getElementById('apellidos').value.trim();
  const correo = document.getElementById('correo').value.trim();
  const telefono = document.getElementById('telefono').value.trim();
  const codigo = document.getElementById('codigo').value;
  const contrasena = document.getElementById('contrasena').value;
  const confirmar = document.getElementById('confirmar').value;

  let valid = true;

  // Validaciones
  if (!nombre) { document.getElementById('errorNombre').style.display = 'block'; valid = false; }
  else { document.getElementById('errorNombre').style.display = 'none'; }

  if (!apellidos) { document.getElementById('errorApellidos').style.display = 'block'; valid = false; }
  else { document.getElementById('errorApellidos').style.display = 'none'; }

  if (!correo.includes('@')) { document.getElementById('errorCorreo').style.display = 'block'; valid = false; }
  else { document.getElementById('errorCorreo').style.display = 'none'; }

  if (!/^\d{4,14}$/.test(telefono)) { document.getElementById('errorTelefono').style.display = 'block'; valid = false; }
  else { document.getElementById('errorTelefono').style.display = 'none'; }

  if (contrasena !== confirmar) { document.getElementById('errorConfirmar').style.display = 'block'; valid = false; }
  else { document.getElementById('errorConfirmar').style.display = 'none'; }

  if (!valid) return;

  const telefonoCompleto = `${codigo} ${telefono}`;
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

    const result = await response.text();
    alert(`Registro exitoso para ${nombre} ${apellidos}.`);
    form.reset();
    passwordStrength.textContent = '';
  } catch (error) {
    alert('No se pudo registrar. Intenta de nuevo más tarde.');
    console.error('Error detallado:', error);
  }
});
