# Programando um jogo estilo ENDLESS RUNNER (corrida infinita), onde o jogador precisa de saltar sobre obstáculos que se aproximam para evitar bater. 

 
Principais Conceitos e Habilidades

        GetComponent 
        ForceMode.Impulse 
        Física.Gravidade 
        Variáveis rigidbody 
        Multiplicar/Atribuir ("*) Operador
        E (&&) Operador / Igual a (==) operador /  Não (!) operador 
  
        OnCollisionEnter()
        Repetir o plano de fundo
        Obter largura collider
        Comunicação de script
        Tags / CompareTag()
         
        Controladores de animação
        Estados de animação, camadas e transições 
        Parâmetros de animação
        Programação de animação
        SetTrigger(), SetBool(), SetInt()
        
        Sistemas de partículas 
        Clipes de áudio e fontes de áudio 
        Reproduzir e parar os efeitos sonoros
<br>
 
Protótipo

        https://connect-prd-cdn.unity.com/20210507/12fe5762-ea5d-48ce-aff7-71c3dd0ec6a0/Prototype%203%20-%20Starter%20Files.zip

Abrir protótipo e alterar fundo

        1) Abra o Unity Hub e crie um projeto vazio de "Protótipo 3" em seu diretório de curso na versão unity correta.
        
        2) Clique para baixar o Protótipo 3 Starter Files, extrair a pasta compactada e, em seguida, importar o .unitypackage em seu projeto. 
         
        3) Abra a cena do Protótipo 3 e exclua a Cena da Amostra sem salvar
        
        4) Selecione o objeto De fundo na hierarquia e, em seguida, no  componente Sprite Renderer > Sprite, selecione a imagem _City, _Nature ou _Town

Escolha e configure um personagem do jogador

        1) Da Biblioteca de Curso > Personagens, Arraste um personagem para a hierarquia, renomeie-o  "Jogador", 
           em seguida, gire-o no eixo Y para enfrentar à direita
           
        2) Adicione um  componente do corpo RigBody     
        
        3) Adicione um colisor de caixa e, em seguida, edite os limites do colisor
        
        4) Crie uma nova pasta "Scripts" em Ativos, crie um script "PlayerController" dentro e conecte-o ao jogador
        
Faça o jogador saltar no início

        1) No PlayerController.cs, declare um novo playerrb rígido privado;  variável
        
        2) Em Start(), inicialize playerRb = GetComponent<Rigidbody>();
        
        3) Em Start(), use o método AddForce para fazer o jogador saltar no início do jogo
        
                public class PlayerController : MonoBehaviour
                {
                    private Rigidbody PlayerRb;
                    void Start()
                    {
                        PlayerRb = GetComponent<Rigidbody>();
                        PlayerRb.AddForce(Vector3.up * 500);
                    }

Faça o jogador saltar se a barra espacial pressionar

        1) Em Atualização() adicione uma instrução if-then verificando se a barra de espaço está pressionada
        
        2) Corte e cole o código AddForce do Start() na instrução if
        
        3) Adicione o  parâmetro ForceMode.Impulse à  chamada AddForce e, em seguida, reduza  o valor do multiplicador de força
        
                    void Update()
                    {
                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            PlayerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                        }
                    }
                }
                
Ajuste a força de salto e a gravidade

        1) Substitua o valor codificado por uma nova  variável pública de salto flutuante

        2) Adicione uma nova  variável de gravidade flutuante públicaModifier e em Start(), 
           adicione Física.gravidade *= gravityModifier; 
        
        3) No inspetor, ajuste os valores de massa gravityModifier, jumpForce e Rigibody 
        
                public class PlayerController : MonoBehaviour
                {
                    public float jumpForce = 10.0f;
                    public float gravityModifier;
                    void Start()
                    {
                        PlayerRb = GetComponent<Rigidbody>();
                        Physics.gravity *= gravityModifier;
                    }

                    void Update()
                    {
                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        }
                    }
                }

Evite que o jogador pule duas vezes

        1) Adicione uma nova variável  de bool isOnGround público e defina-a igual a verdade
        
        2) Na declaração se fazendo o jogador saltar, definir isOnGround = false,
        
        3) Adicione uma condição && isOnGround à declaração if ( && = e , para que haja mais uma afirmação)
        
        4) Adicione um novo metodo vazio OnCollisionEnter, definir isOnGround = verdadeiro nesse método
        
        5) quando o player estiver no chao = true , quando nao tiver = false
        
                public class PlayerController : MonoBehaviour
                {
                    public bool isOnGround = true;
                    
                    void Update()
                    {
                        if (Input.GetKeyUp(KeyCode.Space) && isOnGround)
                        {
                            PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                            isOnGround = false; 
                        }
                    }

                    private void OnCollisionEnter(Collision collision)
                    {
                        isOnGround = true;

                    }
                }
                
Faça um obstáculo e mova-o para a esquerda

            1) Da Biblioteca de Curso > Obstáculos, adicione um obstáculo, renomeie-o como "Obstáculo", e  posicione-o onde deve desovar
            
            2) Aplique um  componente rígido do colisor de corpo e caixa e, em seguida, edite os limites do colisor para se encaixar no obstáculo
            
            3) Crie uma nova pasta "Prefabs" e arraste-a para criar um novo Prefab original
            
            4) Crie um novo script "MoveLeft",  aplique-o ao obstáculo e  abra-o para dar o efeito "parallax"
            
            5) Em MoveLeft.cs, escreva o código para traduzi-lo para a esquerda de acordo com a 
               variável de velocidade. Aplique o script MoveLeft ao fundo(background)
               
                    public class MoveLeft : MonoBehaviour
                    {
                        public float speed = 10.0f;
                        void Start()
                        {

                        }

                        void Update()
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * speed);  
                        }
                    }
                    
Crie um gerenciador de desova

        1) Crie um novo objeto vazio "Spawn Manager" e aplique um novo  script .cs SpawnManager a ele

        2) Em SpawnManager.cs, declare um novo obstáculo público do GameObjectPrefab; , em seguida, 
           atribua seu pré-fabricado à nova variável no inspetor

        3) Declare um novo vetor3 privado spawnPos em seu local de desova
        
        4) In Start(), Instanciar um novo pré-fabricado de obstáculos 
        
        
                public class SpawnManager : MonoBehaviour
                {
                    public GameObject obstaclePrefabs;
                    private Vector3 spawnPosition = new Vector3(25, 0, 0);
                    void Start()
                    {
                        Instantiate(obstaclePrefabs, spawnPosition, obstaclePrefabs.transform.rotation);
                    }
                    
Gerar obstáculos em intervalos

        1) Crie um novo  método de desobstaculo de vazio e, em seguida, mova a  chamada Instantiate dentro dele

        2) Crie novas variáveis flutuantes para iniciarDelay e repetirRate

        3) Que seus obstáculos desovam em intervalos usando o  método InvokeRepeating()

        4) No componente Do corpo rígido do jogador, expanda as restrições e congele  tudo, menos a posição Y
        
                public class SpawnManager : MonoBehaviour
                {
                    public GameObject obstaclePrefabs;
                    private Vector3 spawnPosition = new Vector3(25, 0, 0);
                    private float startDelay = 2.0f;
                    private float repeatRate = 2.0f;
                    void Start()
                    {
                        InvokeRepeating("spawObstacle", startDelay, repeatRate);
                    }

                    void spawObstacle()
                    {
                        Instantiate(obstaclePrefabs, spawnPosition, obstaclePrefabs.transform.rotation);

                    }
                }

<br>

Principais Conceitos e Habilidades modulo 1

        GetComponent
        ForceMode.Impulse
        Física.Gravidade
        Restrições rígidas do corpo
        Variáveis rigidbody
        Booleanos
        Multiplicar/Atribuir ("*) Operador
        E (&&) Operador
        OnCollisionEnter()
        
<br>

Crie um script para repetir o plano de fundo
    
        Crie um novo script chamado RepeatBackground.cs e conecte-o ao objeto de fundo
        
Redefinir posição de fundo

        1) Declarar uma nova variável private Vector3 startPos;

        2) Em Start(), defina a  variável startPos à sua posição inicial real atribuindo-a  = transform.position;

        3) Em Update(), escreva uma posição if-statement para redefinir se ela mover uma certa distância

                public class RepeatBackground : MonoBehaviour
                {
                    private Vector3 startPos;
                    void Start()
                    {
                        startPos = transform.position;
                    }

                    void Update()
                    {
                        if(transform.position.x < startPos.x -50)
                        {
                            transform.position = startPos;
                        }
                    }
                }

Corrigir repetição de fundo com colisor

        1) Adicione um  componente collider de  caixa ao fundo

        2) Declare uma nova  variável de repetição de flutuação privada

        3) Em Start(), obtenha a largura do colisor de caixa, dividido por 2

        4) Incorpore a  variável repetiçãoWidth na função repetição

                public class RepeatBackground : MonoBehaviour
                {
                    private Vector3 startPos;
                    private float reapetWidth;
                    void Start()
                    {
                        startPos = transform.position;
                        reapetWidth = GetComponent<BoxCollider>().size.x / 2;
                    }

                    void Update()
                    {
                        if(transform.position.x < startPos.x - reapetWidth)
                        {
                            transform.position = startPos;
                        }
                    }
                }


Adicione um novo jogo sobre o gatilho

        1) No inspetor, adicione uma tag "Ground" ao chão e uma tag "Obstáculo" ao pré-fio de obstáculo

        2) No PlayerController, declare um novo jogo público boolOver;

        3) Em OnCollisionEnter, adicione a instrução if-else para testar se o jogador colidiu com o "Ground" ou um "Obstáculo"

        4) Se eles colidiram com o "Ground", set isOnGround = verdadeiro, e se colidirem com um "Obstáculo", definir gameOver = verdadeiro        
        
                public class PlayerController : MonoBehaviour
                {
                    public bool gameOver = false;
                    
                    private void OnCollisionEnter(Collision collision)
                    {
                        if (collision.gameObject.CompareTag("Ground"))
                        {
                            isOnGround = true;
                        }
                        else if (collision.gameObject.CompareTag("Obstacle"))
                        {
                            gameOver = true;
                            Debug.Log("Game Over");
                        }

                    }
                }
                
Stop MoveLeft no gameOver

        1) Em MoveLeft.cs, declare um novo player privado PlayerControllerScript;

        2) In Start(), inicialize-o encontrando o Jogador e recebendo o componente PlayerController

        3) Enrole o método de tradução em uma verificação se o jogo não acabou
        
                public class MoveLeft : MonoBehaviour
                {
                    private float speed = 10.0f;
                    private PlayerController playControllerScript;
                    
                    void Start()
                    {
                        playControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
                    }

                    void Update()
                    {
                        if(playControllerScript.gameOver == false)
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * speed);

                        }
                    }
                }
     
 <br>    
 
Conceitos desse modulo 2
 
        Repetir o plano de fundo
        Obter largura collider
        Comunicação de script
        Igual a (==) operador
        Tags
        CompareTag()
        
 <br>       
 
Explore as animações do jogador

        Clique duas vezes no Controlador de Animação do Jogador e, em seguida, 
        explore as diferentes camadas, clicando duas vezes em Estados para ver suas animações 
        e transições para ver suas condições


Faça o jogador começar em uma corrida

        1) Na guia Parâmetros, altere a  variável Speed_f para 1.0

        2) Clique com o botão direito do mouse no Run_Static > Set como Estado padrão da camada

        3) Clique em um único o estado Run_Static e ajuste o  valor de velocidade no inspetor para corresponder à velocidade do fundo


Configure uma animação de salto

        1) No PlayerController.cs, declare um novo jogador privado de AnimatorAnim; 
        
        2) In Start(), set playerAnim = GetComponent<Animator>();

        3) Na declaração if para quando o jogador saltar, acione o salto: playerAnim. SetTrigger ("Jump_trig");
        
        
                public class PlayerController : MonoBehaviour
                {
                    private Rigidbody PlayerRb;
                    private Animator PlayerAnim;

                    void Start()
                    {
                        PlayerRb = GetComponent<Rigidbody>();
                        PlayerAnim = GetComponent<Animator>();
                    }

                    void Update()
                    {
                        if (Input.GetKeyUp(KeyCode.Space) && isOnGround)
                        {
                            PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                            isOnGround = false;
                            PlayerAnim.SetTrigger("Jump_trig");
                        }
                    }



Ajuste a animação de salto

        1) Na janela animador, clique no estado Running_Jump, depois no inspetor e reduza seu valor de velocidade para 0.7

        2) Ajuste a massa do jogador, a força de salto e  o modificador de gravidade para acertar o seu salto
        
        3) player > rigbody > mass > 60, jump force 700, Gravity Modified 1.5;
        

Configure uma animação em queda PlayerController

        1) Na condição de que o jogador colide com Obstáculo, definir a morte bool para a verdade
        
        2) Na mesma declaração se, defina o inteiro DeathType para 1 

                private void OnCollisionEnter(Collision collision)
                {
                    else if (collision.gameObject.CompareTag("Obstacle"))
                    {
                        gameOver = true;
                        Debug.Log("Game Over");
                        PlayerAnim.SetBool("Death_b", true);
                        PlayerAnim.SetInteger("DeathType_int", 1);
                    }

Impedir o jogador de pular inconsciente PlayController

        1) Para evitar que o jogador pule inconsciente, adicione && !gameOver à condição de salto 
        
                void Update()
                {
                    if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
                    {
                        PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isOnGround = false;
                        PlayerAnim.SetTrigger("Jump_trig");
                    }
                    
<br>

Conceitos de modulo 3

        Controladores de animação
        Estados de animação, camadas e transições
        Parâmetros de animação
        Programação de animação
        SetTrigger(), SetBool(), SetInt()
        Não (!) operador
        
 <br>
 
Adicione uma partícula de respingo de sujeira

        1) Da  Biblioteca de curso > Partículas, arraste FX_DirtSplatter para o player

        2) Declarar uma nova sujeira do Sistema de Partículas públicas; , em seguida,  atribuí-lo no Inspetor
        
        3) Adicionar dirtParticle.Stop();  quando o jogador pula ou colide com um obstáculo
        
        4) Adicionar dirtParticle.Play();  quando o jogador pousa no chão
        
                public ParticleSystem dirParticle

                void Update () {
                        if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver){
                                dirtParticle.Stop();  }}

                private void OnCollisionEnter(collision collision other){
                        if (collision.gameObject.CompareTag("Ground")){

                         dirParticle.Play();

                        else if (collision.gameObject.CompareTag("Obstacle"))
                                            dirtParticle.Stop();  }}

    
Adicione música ao objeto da câmera
  
        1) Selecione o  objeto da câmera principal  e, em seguida, adicione componente > áudio source
        
        2) Na Biblioteca de Curso > Sound, arraste um clipe de música para a  variável AudioClip no inspetor
        
        3) Reduza o volume para que seja mais fácil ouvir efeitos sonoros
        
Declare variáveis para clipes de áudio

        1) No PlayerController.cs, declare um novo vídeo público do AudioClip;  e um novo áudioclip público crashSound;

        2) Na Biblioteca de Curso > Som, arraste um clipe para cada nova  variável de som no inspetor
        
Reproduzir clipes de áudio no salto e acidente

        1) Adicione um  componente de fonte de áudio ao player
        
        2) Declare um novo player audiosource privadoAudio;  e inicializá-lo como playerAudio = GetComponent<AudioSource>();

        3) Ligue para o playerAudio.PlayOneShot(jumpSound, 1.0f);  quando o personagem pula

        4) Ligue para o playerAudio.PlayOneShot(crashSound, 1.0f);  quando o personagem trava
        
        
                public class PlayerController : MonoBehaviour
                {
                    private AudioSource PlayerAudio;
                    public AudioClip jumpSound;
                    public AudioClip crashSound;

                    void Start()
                    {
                        PlayerAudio = GetComponent<AudioSource>();
                    }

                    void Update()
                    {
                        if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
                        {
                            PlayerAudio.PlayOneShot(jumpSound, 1.0f);
                        }
                    }

                    private void OnCollisionEnter(Collision collision)
                    {
                        else if (collision.gameObject.CompareTag("Obstacle"))
                        {
                            gameOver = true;
                            PlayerAudio.PlayOneShot(crashSound, 1.0f);
                        }


Novos Conceitos e Habilidades modulo 4

        Sistemas de partículas 
        Posicionamento do objeto infantil
        Clipes de áudio e fontes de áudio 
        Reproduzir e parar os efeitos sonoros
