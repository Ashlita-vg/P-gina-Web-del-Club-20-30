// --- URL DE API ---
const API_URL = 'TU_URL_DE_API_AQUI';

// Toggle contraseña
const passwordInput = document.getElementById('password');
const togglePassword = document.getElementById('togglePassword');
togglePassword.addEventListener('click', () => {
  const isPassword = passwordInput.type === 'password';
  passwordInput.type = isPassword ? 'text' : 'password';
  togglePassword.textContent = isPassword ? 'Ocultar' : 'Mostrar';
});

// Cambiar entre login y registro
const nameGroup = document.getElementById('nameGroup');
const formTitle = document.getElementById('formTitle');
const submitBtn = document.getElementById('submitBtn');
const switchLink = document.getElementById('switchLink');
const mainForm = document.getElementById('mainForm');
const recoverContainer = document.getElementById('recoverContainer');
const forgotLink = document.getElementById('forgotLink');
let isLogin = true;

switchLink.addEventListener('click', () => {
  isLogin = !isLogin;
  if(isLogin){
    nameGroup.style.display='none';
    formTitle.textContent='Iniciar Sesión';
    submitBtn.textContent='Entrar';
    switchLink.textContent='¿No tienes cuenta? Regístrate';
  } else {
    nameGroup.style.display='block';
    formTitle.textContent='Registrarse';
    submitBtn.textContent='Registrarse';
    switchLink.textContent='¿Ya tienes cuenta? Inicia sesión';
  }
});

// Recuperar contraseña
function showRecover() {
  mainForm.style.display='none';
  forgotLink.style.display='none';
  switchLink.style.display='none';
  recoverContainer.style.display='block';
}
function hideRecover() {
  recoverContainer.style.display='none';
  mainForm.style.display='block';
  forgotLink.style.display='block';
  switchLink.style.display='block';
}

// Dropdown
const btn = document.getElementById('menu-btn');
const content = btn.nextElementSibling;
btn.addEventListener('click', (e)=>{ e.stopPropagation(); content.classList.toggle('show'); });
window.addEventListener('click', (e)=>{ if(!btn.contains(e.target) && !content.contains(e.target)) content.classList.remove('show'); });

// Enviar datos a API
mainForm.addEventListener('submit', async (e)=>{
  e.preventDefault();
  const correo = document.getElementById('correo').value.trim();
  const password = document.getElementById('password').value;
  const nombre = document.getElementById('nombre').value.trim();
  const data = isLogin ? { correo, password } : { nombre, correo, password };
  try{
    const response = await fetch(API_URL, {
      method:'POST',
      headers:{ 'Content-Type':'application/json' },
      body:JSON.stringify(data)
    });
    const result = await response.json();
    alert(result.message || 'Operación exitosa');
    if(!isLogin) mainForm.reset();
  }catch(err){
    console.error(err);
    alert('Error al procesar, intenta nuevamente');
  }
});

// Recuperar contraseña
const recoverForm = document.getElementById('recoverForm');
recoverForm.addEventListener('submit', async (e)=>{
  e.preventDefault();
  const correoRec = document.getElementById('correoRec').value.trim();
  try{
    const response = await fetch(`${API_URL}/recuperar`, {
      method:'POST',
      headers:{ 'Content-Type':'application/json' },
      body:JSON.stringify({ correo: correoRec })
    });
    const result = await response.json();
    const messageDiv = document.getElementById('recoverMessage');
    messageDiv.textContent = result.message || 'Revisa tu correo para restablecer contraseña';
    messageDiv.className = 'message success';
  }catch(err){
    console.error(err);
    const messageDiv = document.getElementById('recoverMessage');
    messageDiv.textContent = 'Error al enviar enlace';
    messageDiv.className = 'message error';
  }
});
