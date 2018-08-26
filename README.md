# CalculadoraFreelancer04

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer03">CalculadoraFreelancer03</a>

## Criação do projeto CalculadoraFreelancer.Domain

Vamos criar um projeto chamado CalcFreelancer.Domain do tipo Class Library do .NET Standard. 
Uma Class Library significa que esse projeto não  será executado, somente adicionado em outros projetos.
Esse projeto será responsável por conter nossos domínios, ou seja, nossas entidades e assinaturas de repositório que veremos na próxima aula.

Clique com o botão direito sobre a Solution, vá em Add -> New Project e escolha .NET Standard -> Class Library(.NET Standard). 
Dê um nome para o projeto, chame de de CalcFreelancer.Domain. Em Location, mude o diretório para criação do projeto, pois ele sempre escolhe a pasta raiz.

Clique em Browser e selecione a pasta com o nome do projeto chamado CalcFreelancer. Após, clique em selecionar pasta. Por fim clique em ok.

Abaixo é mostrado esses passos:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_02.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_03.png" alt="Criação projeto >NET Standard" width="100%">

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_criacao_projeto_04.png" alt="Criação projeto >NET Standard" width="100%">

Gif com o processo completo:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Gifs/CalculadoraFreelancer.Domain.gif" alt="Criação projeto >NET Standard" width="100%">

É sempre criado uma classe chamada Class1.cs, vamos a excluir pois não será utilizada. A exclusão não tem segredo, clique sobre o arquivo e aperte delete ou clique com o botão direito e clique em delete.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_exclusao_classe.png" alt="Criação projeto >NET Standard" width="100%">


## Criação do projeto CalcFreelancer.Domain.Core

Vamos repetir o processo anterior, mas agora vamos chamar de CalcFreelancer.Domain.Core. Não se esqueça de alterar o Location.
Esse projeto é responsável por manter as entidades básicas para serem utilizadas no projeto Domain, facilitando seu reuso.

## Criação do projeto CalcFreelancer.Infra.Data

Vamos repetir o processo anterior novamente, mas agora vamos chamar de CalcFreelancer.Infra.Data. Não se esqueça novamente de alterar o Location. Essa camada será responsável pelo acesso a dados, armazenamento e obtenção dos mesmos.

## Estrutura

Nossa estrutura do projeto ficou da seguinte forma:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_estrutura_projeto.png" alt="Criação projeto >NET Standard" width="260">

## Reestruturação dos arquivos

Vamos agora trocar de lugar algumas de nossas classes que estão no projeto principal, vamos distribuí-las nos projetos corretos.

### CalcFreelancer.Domain.Core

Vamos criar dentro do projeto CalcFreelancer.Domain.Core uma pasta chamada Models, dentro dela vamos criar uma classe chamada Entity.

Vamos pensar o que há em comum em todas as nossas entidades que podemos colocar na nossa classe Entity. Todas as nossas entidades tem a propriedade Id, CreatedAt, UpdatedAt e Version. Vamos então criar esses atributos nessa classe. Ficará assim:

```c#
   public class Entity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }
    }
````

Está ocorrendo um erro pois nesse projeto não há a biblioteca do Azure, vamos resolver isso a instalando. 
Clique com o botão direito sobre o projeto CalcFreelancer.Domain.Core e vá em Manage Nuget Packages..., vá em Browse e pesquise por Microsoft.Azure.Mobile.Client, após encontrá-la clique em install.

Após a instalação, vamos na nossa classe Entity e dar o using nesse pacote (aperte ctrl + .  que o visual studio te sugere o using).

### CalcFreelancer.Domain

Vamos criar no projeto CalcFreelancer.Domain uma pasta chamada Profissionais, pois ela representará o nosso domínio relacionado ao profissional. 

Vamos agora arrastar a classe Profissional que está dentro de Models do projeto CalculadoraFreelancer para esta pasta que acabamos de criar. Ao arrastar o Visual Studio cria uma cópia, então apague a que ficou no projeto principal (Dentro de Models, no projeto CalcFreelancer).

Agora nossa classe Profissional pode herdar da classe Entity, vamos então adicionar ao nosso projeto uma referência para o projeto CalcFreelancer.Domain.Core. Dentro do projeto CalcFreelancer.Domain, em Dependencies, clique com o botão direito e vá em Add reference, na tela que irá abrir, vá em Projects e marque o projeto CalcFreelancer.Domain.Core e clique em Ok.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_add_reference.png" alt="Criação projeto >NET Standard" width="300">


<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_add_reference_02.png" alt="Criação projeto >NET Standard" width="100%">

Agora sim, vamos editar a nossa classe Profissional e herdar da classe Entity. Após, o próprio Visual Studio irá nos alertar que há propriedades repetidas entre a Profissional e a Entity, ele faz isso as sublinhando de verde. Vamos remover da classe Profissional as propriedades Id, CreatedAt, UpdatedAt e Version. A classe ficará assim:

```c#
 [DataTable("Profissional")]
    public class Profissional : Entity
    {
        public double ValorGanhoMes { get; set; }
        public int HorasTrabalhadasPorDia { get; set; }
        public int DiasTrabalhadosPorMes { get; set; }
        public int DiasFeriasPorAno { get; set; }
        public double ValorPorHora { get; set; }
    }
````

Vamos repetir o mesmo processo para a classe Projeto, no projeto CalcFreelancer.Domain crie uma pasta chamada Projetos, após, arraste a model Projeto do projeto principal para esta pasta criada. Exclua a pasta Models do projeto principal.

A classe Projeto deve herdar da classe Entity, e excluir os atributos repetidos. A classe ficará assim:

```c#
  [DataTable("Projeto")]
    public class Projeto : Entity
    {
        public string Nome { get; set; }
        public double ValorPorHora { get; set; }
        public int HorasPorDia { get; set; }
        public int DiasDuracaoProjeto { get; set; }
        public double ValorTotal { get; set; }
    }
````

### CalcFreelancer.Infra.Data

Vamos arrastar a pasta Repository do projeto principal para o projeto CalcFreelancer.Infra.Data e após vamos apagar esta pasta do projeto principal.

Vamos adicionar a referência do projeto CalcFreelancer.Domain, vá em Dependencies, clique com o botão direito e vá em Add Reference, escolha Projects e marque o projeto CalcFreelancer.Domain e clique em Ok.

### CalcFreelancer

Agora vamos no projeto principal para adicionar a referência para os projetos CalcFreelancer.Domain e CalcFreelancer.Infra.Data. Vá em Dependencies, clique com o botão direito e vá em Add Reference, escolha Projects e marque os dois projetos ( CalcFreelancer.Domain e CalcFreelancer.Infra.Data) e clique em Ok.

Vamos agora criar uma pasta chamada Views no projeto CalcFreelancer, após, vamos arratas as views que temos atualmente no projeto, a CalculoValorHoraPage.xaml, MainPage.xaml, HomePage.xaml e ProjetoPage.xaml.

### Criação da camada de serviço

Nossas ViewModels já tem a funcionalidade de cuidar das propriedades Bindables e dos Commands, além dos cálculos dos valores, vamos tirar dela a responsabilidade de chamar o Azure, isso será responsabilidade da nossa classe de serviço que vamos criar. No projeto principal crie uma pasta chamada Services, dentro dela crie uma classe chamada ProfissionalService.

Após crie uma propriedade nessa classe chamada ProfissionalRepository, dessa forma:

```c#
   public class ProfissionalService
    {
        private readonly AzureRepository ProfissionalRepository;
    }
````

Agora crie um construtor para essa classe e vamos instanciar um objeto do tipo AzureRepository e entregar para a propriedade que criamos, dessa forma:

```c#
   public class ProfissionalService
    {
        private readonly AzureRepository ProfissionalRepository;

        public ProfissionalService()
        {
            ProfissionalRepository  = new AzureRepository();
        }
    }
````

Agora vamos criar a função de Inserir, que chamará a função Insert do ProfissionalRepository. A classe ficou dessa forma:

```c#
    public class ProfissionalService
    {
        private readonly AzureRepository ProfissionalRepository;

        public ProfissionalService()
        {
            ProfissionalRepository  = new AzureRepository();
        }

        public void Inserir(Profissional profissional)
        {
            ProfissionalRepository.Insert(profissional);
        }
    }
````

Agora na CalculoValorHoraPageViewModel.cs vamos ter uma propriedade do tipo ProfissionalService que será iniciada no construtor dessa classe:

```c#
    public class CalculoValorHoraPageViewModel : ViewModelBase
    {
         ...

        private readonly ProfissionalService ProfissionalService;

        public CalculoValorHoraPageViewModel()
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            Profissional = new Profissional();
            ProfissionalService = new ProfissionalService();
        }
        
        ...
    }
````

Agora na função ExecuteGravarCommand vamos chamar o nosso service no lugar de acessar diretamentne o repositório. Ficará assim:

```c#
  private async void ExecuteGravarCommand(object obj)
  {
      Profissional.ValorGanhoMes = ValorGanhoMes;
      Profissional.HorasTrabalhadasPorDia = HorasTrabalhadasPorDia;
      Profissional.DiasTrabalhadosPorMes = DiasTrabalhadosPorMes;
      Profissional.DiasFeriasPorAno = DiasFeriasPorAno;
      Profissional.ValorPorHora = ValorDaHora;

      ProfissionalService.Inserir(Profissional);

      await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
  }
````

Vamos agora fazer o mesmo para o Projeto. Crie dentro da pasta services uma classe chamada ProjetoService. Nessa classe crie uma propriedade do tipo AzureProjetoRepository e no construtor da classe inicie essa propriedade.

```c#
  public class ProjetoService
    {
        private readonly AzureProjetoRepository ProjetoRepository;

        public ProjetoService()
        {
            ProjetoRepository = new AzureProjetoRepository();
        }
    }
````

Agora crie a função inserir para gravar o projeto no nosso repositório. A classe completa ficará assim:

```c#
   public class ProjetoService
    {
        private readonly AzureProjetoRepository ProjetoRepository;

        public ProjetoService()
        {
            ProjetoRepository = new AzureProjetoRepository();
        }

        public void Inserir(Projeto projeto)
        {
            ProjetoRepository.Insert(projeto);
        }
    }
````

Altere a ViewModel ProjetoPageViewModel da mesma forma que fizemos com a CalculoValorHoraPageViewModel. Crie uma propriedade do tipo ProjetoService e a inicie no construtor. 

```c#
  public class ProjetoPageViewModel : ViewModelBase
    {
         ...      

        private readonly ProjetoService ProjetoService;

        public ProjetoPageViewModel()
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            LimparCommand = new Command(ExecuteLimparCommand);
            ProjetoService = new ProjetoService();
        }
        
        ...
        
     }
````

Agora na função ExecuteGravarCommand ao invés de chamar o AzureProjetoRepository troque pela chamada ao nosso service.

```c#
     private async void ExecuteGravarCommand()
        {
            ProjetoService.Inserir(new Models.Projeto()
            {
                ValorPorHora = ValorPorHora,
                HorasPorDia = HorasPorDia,
                DiasDuracaoProjeto = DiasDuracaoProjeto,
                ValorTotal = ValorTotal
            });


            ExecuteLimparCommand();

            await App.Current.MainPage.DisplayAlert("Sucesso", "Projeto gravado!", "Ok");
        }
````

## Resultado

Reestruturamos nosso projeto, ficando melhor dividido. A estrutura de pastas final ficou assim:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/aula_04_estrutura_pasta_final.png" alt="Criação projeto >NET Standard" width="260">

## Converters em Xamarin Forms

No Xamarin Forms há os conhecidos Converter que são bastante utilizados em conjunto com o MVVM. 

Quando temos valores de moeda, na nossa tela desejamos que esse valor apareça bonito para o nosso usuário, com o R$, casas decimais, tudo correto, mas se estamos utilizando o MVVM, esse valor de moeda na nossa ViewModel é do tipo double (ou algum tipo relacionado, como o float). Então como exibir um valor na tela para o usuário e ao mesmo tempo esse valor corresponder a tipagem da variável na ViewModel, ou seja, exibir a mesma informação, mas formatada diferente? Através dos Converters!!

Para criar um converter você irá implementar de IValueConverter, que te obrigará a implementar os métodos Convert e ConvertBack. A funçãoa Convert irá entregar o valor que será exibido na View, já a função ConvertBack entregará o valor que será atribuido a variável Bindable na ViewModel.

###  Criando o Converter para Moeda

Vamos colocar os converters em prática criando um para moeda. 

Crie uma pasta no projeto principal chamada Converters. Após, crie uma classe chamada de MoedaConverter:

```c#
   public class MoedaConverter : IValueConverter
    {
       
    }
````

Ele dará um erro obrigando a implementar as funções Convert e ConvertBack.

```c#
   public class MoedaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
````

No Converter queremos receber um valor e convertê-lo para uma string no formato de moeda, pois é o que queremos exibir na nossa View. Ficará assim:

```c#
 public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
      return Decimal.Parse(value.ToString()).ToString("C");
  }
````

Agora queremos obter o valor que está na tela e convertê-lo para double, para que nossa ViewModel tenha o valor correto em sua variável Bindable. Vamos alterar a função ConvertBack, ela ficará assim:

```c#
 public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
      string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

      if (valueFromString.Length <= 0)
          return 0m;

      long valueLong;
      if (!long.TryParse(valueFromString, out valueLong))
          return 0m;

      if (valueLong <= 0)
          return 0m;

      return valueLong / 100m;
  }
````

A classe completa ficará assim:

```c#
    public class MoedaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Decimal.Parse(value.ToString()).ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
````

### Adicionando o Converter nos nossos xaml

Vamos editar o arquivo chamado ProjetoPage.xaml. Primeiramente vamos adicionar uma referência para nossos converters, então na tag do ContentPage, adicione o seguinte trecho de código:

```xml
 xmlns:converters="clr-namespace:CalcFreelancer.Converters"
````

Ficará assim:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer.Views.ProjetoPage"
             xmlns:converters="clr-namespace:CalculadoraFreelancer.Converters"
             Icon="money.png"
             Padding="10">
````

Agora temos que criar uma Key para acessar esse Converter, para isso precisamos declará-lo no ResourceDictionary da nossa tela, dessa forma:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer.Views.ProjetoPage"
             xmlns:converters="clr-namespace:CalculadoraFreelancer.Converters"
             Icon="money.png"
             Padding="10">
   
       <ContentPage.Resources>
           <ResourceDictionary>
               <converters:MoedaConverter x:Key="MoedaConverter" />
           </ResourceDictionary>
       </ContentPage.Resources>
   
   ...
````

Agora no Text do Entry de valor por hora vamos chamar nosso Converter, dessa forma:

```xml
       <Entry Placeholder="Valor por hora"
                   Keyboard="Numeric"
                   Text="{Binding ValorPorHora, Converter={StaticResource MoedaConverter}}"/>
````

Nossa tela completa ficou dessa forma:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetoPage"
             Title="Projeto"
             Padding="10">
    <ContentPage.Content>
        <StackLayout>

            <Label Text="Nome do Projeto" />
            <Entry Placeholder="Nome do projeto"
                   Text="{Binding Nome}" />

            <Label Text="Valor por hora" />
            <Entry Placeholder="Valor por hora"
                   Keyboard="Numeric"
                   Text="{Binding ValorPorHora, Converter={StaticResource MoedaConverter}}"/>

            <Label Text="Horas por dia" />
            <Entry Placeholder="Horas por dia"
                   Keyboard="Numeric"
                   Text="{Binding HorasPorDia}"/>

            <Label Text="Dias de duração do projeto" />
            <Entry Placeholder="Dias de duração do projeto"
                   Keyboard="Numeric"
                   Text="{Binding DiasDuracaoProjeto}"/>

            <Label FontSize="Large"
                   Text="{Binding ValorTotal, StringFormat='Total: {0:C}'}"/>

            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>

                <Button Grid.Column="0"
                        BackgroundColor="#cdcdcd"
                        Text="Limpar"
                        Command="{Binding LimparCommand}"/>

                <Button Grid.Column="1"
                        Text="Gravar"
                        TextColor="White"
                        BackgroundColor="#6699ff"
                        Command="{Binding GravarCommand}"/>

            </Grid>

        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

Vamos fazer o mesmo na view CalculoValorHoraPage. Adicione a referência do converter no arquivo xaml:

```xml
 xmlns:converters="clr-namespace:CalcFreelancer.Converters"
````

Adicione no ResourceDictionary da view a referência para o nosso converter:

```xml
   <ContentPage.Resources>
        <ResourceDictionary>
            <converters:MoedaConverter x:Key="MoedaConverter" />
        </ResourceDictionary>
    </ContentPage.Resources>
````

Agora no Entry de Valor ganho por mês adicione o nosso converter:

```xml
 <Entry Placeholder="Valor ganho por mês"
                  Text="{Binding ValorGanhoMes, Converter={StaticResource MoedaConverter}}"
                  Keyboard="Numeric"/>
````

Ficando assim a nossa tela completa:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.CalculoValorHoraPage"             
             xmlns:converters="clr-namespace:CalcFreelancer.Converters"
             Title="Valor da Hora"
             Padding="10">

    <ContentPage.Resources>
        <ResourceDictionary>
            <converters:MoedaConverter x:Key="MoedaConverter" />
        </ResourceDictionary>
    </ContentPage.Resources>

    <ContentPage.Content>
        <StackLayout>

            <Label Text="Valor ganho por mês" />
            <Entry Placeholder="Valor ganho por mês"
                  Text="{Binding ValorGanhoMes, Converter={StaticResource MoedaConverter}}"
                  Keyboard="Numeric"/>

            <Label Text="Horas trabalhadas por dia" />
            <Entry Placeholder="Horas trabalhadas por dia"
                  Text="{Binding HorasTrabalhadasPorDia}"
                  Keyboard="Numeric"/>

            <Label Text="Dias trabalhados por mês" />
            <Entry Placeholder="Dias trabalhados por mês"
                  Text="{Binding DiasTrabalhadosPorMes}"
                  Keyboard="Numeric"/>

            <Label Text="Dias de férias por ano" />
            <Entry Placeholder="Dias de férias por ano"
                  Text="{Binding DiasFeriasPorAno}"
                  Keyboard="Numeric"/>

            <Label Text="{Binding ValorDaHora, StringFormat='{0:C} / hora'}"
                     FontSize="Large"
                     TextColor="Green"/>

            <Button Text="Gravar"
                   BackgroundColor="#6699ff"
                   TextColor="White"    
                   Command="{Binding GravarCommand}"/>


        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

## Adicional: Lottie para Xamarin Forms

### O que é Lottie?

Lottie é uma biblioteca da Airbnb para renderizar animações feitas com o After Effects em seus projetos. Há implementações para diverasas plataformas, inclusive o Xamarin!!!

Para saber mais acesse: <a target='_blank' href='https://airbnb.design/introducing-lottie'>airbnb.design</a>

Para quem não quer criar as animações no After Effects, existe um repositório de animações prontas nesse link: <a  target='_blank' href='https://www.lottiefiles.com/'>lottiefiles</a> 

### Adicionando o Lottie ao seu projeto Xamarin

Primeiro vamos baixar a biblioteca, para isso clique com o botão direito sobre a Solution e vá em Manage Nuget Packages For Solution...

Em Browse pesquise por Com.Airbnb.Xamarin.Forms.Lottie

Ao encontrá-la, no canto direito escolha em quais projetos deseja instalar a biblioteca, escolha o projeto principal, o Android e o IOS.  Após clique em install:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Imgs/instalando_lottie_01.png" alt="Instalação Lottie" width="100%">

### Criando a View para exibir a animação

Ao clicar em gravar na nossa CalculoValorHoraPage, ao invés de exibirmos apenas um alerta, vamos navegar para outra tela e exibir uma mensagem de sucesso que terá uma animação.

Primeiro vamos criar uma View chamada CalculoValorHoraSucessoPage. Vá na nossa pasta View no projeto principal, clique com o botão direito sobre ela e vá em Add -> New Item.. Escolha Xamarin Forms e marque a Content Page e dê o nome de CalculoValorHoraSucessoPage.xaml. 

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Gifs/CriacaoTelaSucesso.gif" alt="Criação Tela Sucesso" width="100%">

Agora vamos baixar a animação que vamos utilizar, acesse o link abaixo para fazer o download:

<a  target='_blank' href='https://www.lottiefiles.com/download/782'>baixar animação</a> 

Após, extraia o Zip e ache o arquivo com a extensão JSON.

Então arraste este arquivo encontrado para a pasta Assets no projeto CalcFreelancer.Android.

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Gifs/ArrastarJsonLottie.gif" alt="Criação Tela Sucesso" width="100%">

Agora voltando a nossa tela CalculoValorHoraSucessoPage, vamos criar o layout dela.

Ela terá uma referência a biblioteca do Lottie que baixamos. 

```xml
    xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
````

Ficando assim:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
             x:Class="CalcFreelancer.Views.CalculoValorHoraSucessoPage">
   
   ...
   
````

Ela terá um StackLayout para ficar centralizado.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
             x:Class="CalcFreelancer.Views.CalculoValorHoraSucessoPage">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical"
                     VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

Dentro do stacklayout teremos a nossa animação. 

É um elemento do tipo AnimationView. 

Na propriedade Animation devemos colocar o nome do arquivo JSON que baixamos. 

A propriedade Loop define se a animação sempre ficará sendo executada ou não. 

A AutoPlay define se a animação será executada assim que a tela for aberta. 

Devemos definir uma altura e largura para a animação, ou seja, as propriedades HeightRequest e WidthRequest.

E vamos deixá-la no centro da tela através das propriedades HorizontalOptions e  VerticalOptions com o valor Center.

Ficando assim:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
             x:Class="CalcFreelancer.Views.CalculoValorHoraSucessoPage">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical"
                     VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
            <lottie:AnimationView 
	                  Animation="Check Mark Success Data.json" 
	                  Loop="false" 
	                  AutoPlay="true"	       
                     HeightRequest="100"
                     WidthRequest="100"
	                  HorizontalOptions="Center"
          	         VerticalOptions="Center"/>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

Teremos uma Label com a mensagem de sucesso:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
             x:Class="CalcFreelancer.Views.CalculoValorHoraSucessoPage">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical"
                     VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
            <lottie:AnimationView 
	                  Animation="Check Mark Success Data.json" 
	                  Loop="false" 
	                  AutoPlay="true"	       
                     HeightRequest="100"
                     WidthRequest="100"
	                  HorizontalOptions="Center"
          	         VerticalOptions="Center"/>
            <Label Text="Valor por hora gravado!"
                   FontSize="Large"
                   TextColor="Green"
                   FontAttributes="Bold"
                   VerticalOptions="Center"
                   HorizontalOptions="Center"/>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

Por fim um botão de voltar. Esse botão terá o atributo Clicked que chamará uma função diretamente do Code Behind, feito somente para ganharmos tempo.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:lottie="clr-namespace:Lottie.Forms;assembly=Lottie.Forms"
             x:Class="CalcFreelancer.Views.CalculoValorHoraSucessoPage">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical"
                     VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
            <lottie:AnimationView 
	                Animation="Check Mark Success Data.json" 
	                Loop="false" 
	                AutoPlay="true"	       
                    HeightRequest="100"
                    WidthRequest="100"
	                HorizontalOptions="Center"
          	        VerticalOptions="Center"/>
            <Label Text="Valor por hora gravado!"
                   FontSize="Large"
                   TextColor="Green"
                   FontAttributes="Bold"
                   VerticalOptions="Center"
                   HorizontalOptions="Center"/>

            <Button Text="Ok"
                    HorizontalOptions="Center"
                    BackgroundColor="#6699ff"
                    TextColor="White"  
                    Clicked="Button_Clicked"
                    Margin="0,20,0,0"/>

        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````
No Code Behind (CalculoValorHoraSucessoPage.xaml.cs) terá a função de voltar para a tela anterior:

```c#
public partial class CalculoValorHoraSucessoPage : ContentPage
{
     public CalculoValorHoraSucessoPage ()
     {
        InitializeComponent ();
     }

     private void Button_Clicked(object sender, EventArgs e)
     {
         App.Current.MainPage.Navigation.PopAsync();
     }
}
````

Na nossa tela não queremos que apareca a nossa barra de navegação, para removê-la basta no construtor do Code Behind setar o valor do SetHasNavigationBar para false, dessa forma:

```c#
public CalculoValorHoraSucessoPage ()
{
      InitializeComponent ();
      NavigationPage.SetHasNavigationBar(this, false);
}
````

O Code Behind ficou assim:

```c#
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CalculoValorHoraSucessoPage : ContentPage
{
         public CalculoValorHoraSucessoPage ()
         {
               InitializeComponent ();
                NavigationPage.SetHasNavigationBar(this, false);
         }

         private void Button_Clicked(object sender, EventArgs e)
         {
             App.Current.MainPage.Navigation.PopAsync();
         }
}
````

Agora na nossa CalculoValorHoraPageViewModel.cs, na função ExecuteGravarCommand, ao invés de exibimos o alerta de sucesso, vamos navegar para a nossa página de sucesso, ficando dessa forma:

```c#
public class CalculoValorHoraPageViewModel : ViewModelBase
{
    
    ...
    
   private async void ExecuteGravarCommand(object obj)
   {
            Profissional.ValorGanhoMes = ValorGanhoMes;
            Profissional.HorasTrabalhadasPorDia = HorasTrabalhadasPorDia;
            Profissional.DiasTrabalhadosPorMes = DiasTrabalhadosPorMes;
            Profissional.DiasFeriasPorAno = DiasFeriasPorAno;
            Profissional.ValorPorHora = ValorDaHora;

            ProfissionalService.Inserir(Profissional);

            await App.Current.MainPage.Navigation.PushAsync(new CalculoValorHoraSucessoPage());
            //await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
    }
    
    ...
    
}
````

## Resultado

<img src="https://github.com/dayaneLima/CalculadoraFreelancer04/blob/master/Docs/Gifs/ResultadoAppTelaSucesso.gif" alt="App coma tela de sucesso" width="260">
