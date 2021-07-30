using Moq.AutoMock;
using Servico;

namespace Testes.Servicos.Fixtures
{
    public class ProtutoServiceFixture
    {
        public AutoMocker Mocker;
        public ProdutoService Service;
        public ProtutoServiceFixture()
        {
            Mocker = new AutoMocker();
            Service = InstanciarService();
        }

        public ProdutoService InstanciarService() => Mocker.CreateInstance<ProdutoService>();
    }
}
