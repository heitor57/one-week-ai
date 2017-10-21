using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOCUMENTAÇAO : MonoBehaviour {

	/*
	 * Este script servira para dar o maximo de informações sobre coisa que estão repetidas em todos os scripts
	 * Scripts criados por Juan Pabllo Miguel, para o trabalho de PFC do CEFET-MG Leopoldina
	 * 
	 * Primeira mente todos os scripts são herdados do script Destrutiveis, incluise os scripts feito por Moyses Caetano
	 * também deverão ser herdados desta mesma classe.
	 * Motivo: A principio tudo do jogo vai ser destrutivel, por isto criei uma classe que contesse os metodos necessarios
	 * para todas as outras, para evitar o trabalho de copiar o mesmo metodo por dezenas de classes.
	 * 
	 * A maioria das classes por enquanto estão sendo criadas como classes abstratas,
	 * para que não se possa criar um objeto que seja apenas daquela classe.
	 * 
	 * Todos os atributos protected, tem um comando [SerializeField] antes dele, para que durante os testes no unity, 
	 * esses atributos possam ser vistos.
	 * 
	 * Informações sobre os scripts: Estas informações não conterão metodos que fazem a mesma coisa so que com nomes diferente.
	 * irei dizer sobre eles no final do documento.
	 * 
	 * Destrutives:
	 * 	Primeira classe, classe mais importante do desenvolvimento ja que todas as outras são herdadas dela.
	 * 	Classe abstrata.
	 * 	Como já dito classe nescessaria para a destruição dos demais objetos.
	 * 	Nesse script contem os seguintes atributos globais protegidos:
	 * Herdada da classe padrão do Unity MonoBehaviour.
	 * 
	 * 		vida: Atributo que controla o dano que um objeto pode tomar antes de ser destruido,
	 * 		tambem pode ser considerado durabilidade, porém não encontrei outro nome que poderia ser utilizado para os dois.
	 * 
	 *	 	dano:Atributo que diminuiria o atributo vida aos poucos, conforme o objeto foste alvejado.
	 * 
	 * 		nome:Atributo que daria o nome ao objeto, 
	 *	 	ainda não foi feito um jeito de mudar automaticamente o nome do objeto dentro do unity.
	 * 
	 *		 rnd: um numero que sera aleatoriamente para escolher um nome para o objeto,
	 * 		a questão de o numero ser aleatorio não está funcionando 100%
	 * 		este numero seria escolhido aleatoriamente, porém ele sera escolhido de acordo com o objeto,
	 * 		por exemplo um Ser Humano não teria o nome de uma planta e assim por diante.
	 * 
	 * 	Nesse script contem os seguintes metodos publicos,junto com seus atributos:
	 * 		
	 * 		int GetRnd: Metodo que sorteia o numero para a escolha do nome.
	 * 					Comando: UnityEnfine.Random.Range, serve para importar a classe Random responsavel por sortear um numero
	 * 							 Range comando que sorteia o numero entre o primeiro parametro e o ultimo que podem ser escolhidos.
	 * 
	 * 		void perdeVida: Metodo utilizado para diminuir o atributo vida de acordo com o dano sofrido e destruir o objeto
	 * 						Parametros: double dano: atributo que pega o valor do dano sofrido e utilizado para retirar a vida do objeto.
	 * 
	 * 		void OnTriggerEnter: Metodo utilizado para testar o sistema de perda de vida, e que por enquanto vai ser o utilizado para dar
	 * 							 dano nos combates, já que ainda não sei como usar a animação para isto.
	 *
	 *Script SerVivo:
	 *Outro script super importante para o projeto, todos os scripts feitos por min, menos o Destrutiveis, são herdados desta classe.
	 *Classe também abstrata.
	 *Classe que denomina todos os objetos com que possam ter vida, ainda podendo estar incompleto.
	 *Herade de Destrutiveis.
	 *
	 *	Nessa classe se encontra os seguintes atributos globais protegidos:
	 *
	 *		velocidade: atributo que controlará o poder de locomoção do objeto, se ele é mais rapido, mais lento, etc...
	 *
	 *		sexo: atributo que define se o Ser Vivo vai ser masculino ou feminino.
	 *
	 *		cor: atributo que define a etnia do Ser Vivo, por exemplo se ele é branco ou negro.
	 *
	 *		idade: atributo que define a idade do Ser Vivo, por exemplos se ele vai ter 40 ou 10 anos.
	 *
	 *	Nesta classe se encontra os seguintes metodos publicos:
	 *	Estes metodos estão como string pois o principal papel deles por agora é mostrar se o objeto está realizando ou não a ação,
	 *	não vao ter parametros pois ele se utilizaram dos seus proprios atributos, ou dos atributos que a classe destrutivel possa ter.
	 *		
	 *		string Parado: como o proprio nome já diz, mostra se o objeto está parado.
	 *
	 *		string Andar: como o proprio nome já diz, mostra se o objeto está andando.
	 *
	 *		string Correndo: como o proprio nome já diz, mostra se o objeto está correndo.
	 *
	 *		string SeguirRota: este metodo foi planejado seguindo o raciocinio de que haveria rotas pelo mapa para que o objeto
	 *						   possa percorrer, e irá mostrar se ele está seguindo ou não a rota.
	 *
	 *Script Vegetaçao:
	 *Herdada de SerVivo
	 *Script basico, para apenas diferenciar os tipos de Seres Vivos.
	 *	Contem o atributo folha, que dirá qual o tipo que a folha irá ter.
	 *	Por enquanto sem nenhum metodo
	 *
	 *Script Graminha:
	 *Herada de Vegetaçao.
	 *Script básico, para diferenciar as arvores das graminhas.
	 *	Por enquanto sem nenhum atributo ou metodo.
	 *
	 *Scripts herdados da Graminha.
	 *Irei falar sobre os trés (Venenosa, Normal,Medicinal) fazerem a mesma coisa so que com valores diferentes.
	 *Primeira leva de scripts feita que não será abstrata.
	 *	Neste script contem o atributo Nomes n: que será o responsavel por passar os possiveis nomes que os objetos possam ter,
	 *	não foi escolhido o atributo nome, pois na classe Destrutiveis já existe um atributo nome, ou seja, para não sobreescrever.
	 *	Será abordado mais a frente.
	 *
	 *	Metodos:
	 *		void Start: que esta preechendo os valores de todas as variaveis de que os seus atributos são herdados,
	 *		como nome,idade,cor e assim por diante
	 *		void update: sem nenhuma interaçao no momento.
	 *
	 *	Diferença entre as três:
	 *		O estilo de grama normal, vai ser a grama que sera utilizada em missões.
	 *		O estilo de grama venenosa, vai ser a encontrada ou comprada que ou faz voce tomar dano.
	 *		O estilo de grama medicinal, vai ser encontrada ou comprada e irá fazer voce se curar.
	 *
	 *Script Arvore:
	 *Herdada de Vegetaçao.
	 *Script que dara o nome cor e idade e outros atributos que ela tem herdado de outras classes as arvores.
	 *
	 *		Como no das graminhas tem um atributo Nomes n para definir os nomes que a arvore possa ter.
	 *
	 *	Metodos:
	 *		void Start: está preechendo os valores de todas as variaveis e atributos que foram herdados.
	 *		Informação adicional sobre este metodo dentro desse metodo, dentro do script.
	 *		void Updade: sem funcionalidade por enquanto.
	 *
	 *Script Nomes:
	 *Script que tera contindo em seu interios os nomes que os objetos possão ter.
	 *	Atributos:
	 *		
	 *		ArrayList Nome: atributo que guardará todas as possibilidades de nomes.
	 *		o motivo de ser um arrray lis e não um vetor, e por ter metodos que facilitão a utilizaçao,
	 *		e que aumenta conforme sua necessidade.
	 *
	 *	Metodos:
	 *
	 *		string GetNome:Metodo que retornará os possiveis nomes, alem de armagenar os possiveis nomes,
	 *		motivo de não ter feito no start, sem motivo. 
	 *		Parametros: int i que será o valor sorteado pelo rnd da classe Destrutiveis, para que possa ser
	 *		escolhido o nome nessa posição.
	 *
	 *Script SerHumano:
	 *Atributo que controla o piscicologico dos seres humanos e que tem algumns metodos de interações.
	 *Classe abstrata.
	 *Herdada de SerVivo.
	 *
	 *	Atributos protegidos:
	 *
	 *		bondade, carisma, coragem, influencia ,profissão e lealdade, são os atributos que moldão o piscicologico dos seres humanos.
	 *		Ainda sem nenhuma interaçao ou metodos que os utilize.
	 *
	 *	Metodos:
	 *	Estes metodos que estão como string como já dito na classe SerVivo	são para mostrar que o SerHumano está fazendo está interação,
	 *	ainda sem parametros por não saber como será o jeito que lidaremos com a classe.
	 *
	 *		string Interagir: mostra o SerHumano fazendo alguma interação.
	 *
	 *		string Montar: interação que vai fazer com que o usuario monte em seu cavalo, ainda não ligado a classe Cavalo para utilização.
	 *
	 *		string TerAnimais: interação que irá mostrar o SerHumano pegando algum animal para si.
	 *
	 *		string TerFilhos: interação que mostrará o SerHumano fazendo um filho, por enquanto como lidar com ela ainda indefinido.
	 *
	 *		string MeusAnimais: usado para mostrar os animais que o ser humano possui, por enquanto ainda nao conectado a classe Animais.
	 *
	 *		string Comandar: usado para dizer que o Ser Humano está mandando em alguem.
	 *
	 *Script Animal:
	 *Script para separa os animais de todos os outros seres vivos.
	 *Classe abstrata.
	 *Herdada de SerVivo.
	 *
	 *	Atributos protegidos:
	 *	
	 *		Lealdade: No futuro irá servir para as interações com o animal dependendo deste valor.
	 *
	 *	Sem nenhum metodo expecifico.
	 *
	 *
	 *
	 *Script Cavalo:
	 *Script relacionado aos cavalos.
	 *Herdada de Animal.
	 *
	 *	Atributo Nomes n: que servirá para dar um nome aleatorio para o animal conforme foi feito em varias outras classes.
	 *
	 *	Sem nenhum metodos expecifico.
	 *
	 *Script Revoada:
	 *Script relacionado aos passaros.
	 *Herdado de Animal.
	 *
	 *	Atributo Nomes n: que servirá para dar um nome aleatorio para o animal conforme foi feito em varias outras classes.
	 *
	 *	Metodo:
	 *
	 *		string Voar: utilizado por essa classe por serem os unicos seres capazes de voar no jogo.
	 *
	 *
	 *Script Gado:
	 *Script relacionado ao gado.
	 *Herdado de Animal.
	 *
	 *	Atributo Nomes n: que servirá para dar um nome aleatorio para o animal conforme foi feito em varias outras classes.
	 *
	 *	Metodo:
	 *
	 *		string Pastar: utilizado para mostrar que este animal vai estar pastando.
	 *
	 *Script Cachorro:
	 *Script relacionado aos cachorros.
	 *Herdado de Animal.
	 *
	 *	Atributo Nomes n: que servirá para dar um nome aleatorio para o animal conforme foi feito em varias outras classes.
	 *
	 *	Metodo:
	 *
	 *		string Atacar: utilizado para quando o personagem principal passar perto do animal, para que o cachorro o ataque.
	 *
	 *Script Realeza:
	 *Script relacionado aos membros da realeza.
	 *Classe abstrata.
	 *Herdado de SerHumano.
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para esta classe.
	 *
	 *Script Rei e Script Rainha
	 *Scripts relacionados ao rei e a rainha
	 *Herdado de Realeza.
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para estas classes.
	 *
	 *Script Sudito.
	 *Script relacionado a todos os suditos.
	 *Classe abstrata.
	 *Herdado de SerHumano.
	 *
	 *	Metodo: 
	 *		
	 *		string Servir: que sera utilizado quando o sudito estiver servindo alguem da realeza.
	 *
	 *Script Clero.
	 *Script relacionado aos padres, bispos e coisas do tipo.
	 *Herdado de SerHumano.
	 *
	 *	Metodo:
	 *	
	 *		string Ensinar: que será utilizado para mostrar quando alguem do clero estiver dando aula.
	 *
	 *Script Nativo.
	 *Script relacionado aos nativos do jogo.
	 *
	 *	Metodo:
	 *
	 *		string FazerRemedio: que será utilizado quando o nativo estiver preparando uma poção.
	 *
	 *Script Nobre.
	 *Script relacionado a todos os nobres do jogo
	 *Herdado de Sudito.
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para esta classe.
	 *
	 *Script Plebeu.
	 *Script relacionado aos camponeses e burgueses do jogo.
	 *Herdado de Sudito.
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para esta classe.
	 *
	 *Script Campones.
	 *Script relacionado aos camponeses.
	 *Herdado de Plebeu.
	 *
	 *	Metodos:
	 *
	 *		string Cultivar: utilizado quando o campones estiver plantando, colhendo, regando ou coisas do tipo.
	 *
	 *		string Forjar: utilizado quando o campones estiver fazendo roupas, armaduras, armas, escudos e etc.
	 *
	 *		string Construir: utilizado quando o campones estiver construindo uma casa, estabelecimento e etc.
	 *
	 *Script Burgues.
	 *Script relacionado aos burgueses.
	 *Herdado de Plebeu.
	 *
	 *	Metodos:
	 *
	 *		string Vender: metodo para mostrar que o burgues esta vendendo alguma coisa.
	 *
	 *
	 *Script Cavaleiro.
	 *Script relacionado aos capitaes, regente e escudeiros.
	 *Herdado de Nobre
	 *
	 *	Metodo:
	 *
	 *		string Defender: utilizado quando o cavaleiro estiver defendendo alguem ou algum lugar.
	 *
	 *Script Corte.
	 *Script relacionado aos membros da corte.
	 *Entrou no lugar das classes Barao, Conde, Duque e etc.
	 *Classe que vai ter seus privilegios alterados conforme o atributo profissão, que o ser humano possui.
	 *Herdado de Nobre.
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para esta classe.
	 *
	 *Scripts Capitao, Regente e Escudeiro
	 *Script relacionado aos soldados.
	 *Herdado de Nobre
	 *
	 *	Por enquanto script vazio por não saber metodos e atributos que poderiam ser unicos para esta classe.
	 *
	 *
	 *
	 *
	 *
	 *
	 *Metodos que não foram citados nesta documentação, são os metodos que começam com Get, ou com Set, estes metodos servem para
	 *armazenar e mostrar respectivamente os valores dos atributos.
	 */
}
