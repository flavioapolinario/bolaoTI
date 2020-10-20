using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BolaoTI.web.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BolaoTIContext context = new BolaoTIContext();

        private GenericRepository<Fase> faseRepository;
        private GenericRepository<Grupo> grupoRepository;
        private GenericRepository<Partida> partidaRepository;
        private GenericRepository<Time> timeRepository;
        private GenericRepository<Estadio> estadioRepository;
        private GenericRepository<Rodada> rodadaRepository;
        private GenericRepository<UserProfile> usuarioRepository;

        private ApostaRepository apostaRepository;
        private RankingRepository rankingRepository;

        public GenericRepository<Fase> FaseRepository
        {
            get
            {

                if (this.faseRepository == null)
                {
                    this.faseRepository = new GenericRepository<Fase>(context);
                }
                return faseRepository;
            }
        }

        public GenericRepository<Grupo> GrupoRepository
        {
            get
            {

                if (this.grupoRepository == null)
                {
                    this.grupoRepository = new GenericRepository<Grupo>(context);
                }
                return grupoRepository;
            }
        }

        public GenericRepository<Partida> PartidaRepository
        {
            get
            {

                if (this.partidaRepository == null)
                {
                    this.partidaRepository = new GenericRepository<Partida>(context);
                }
                return partidaRepository;
            }
        }

        public GenericRepository<Time> TimeRepository
        {
            get
            {

                if (this.timeRepository == null)
                {
                    this.timeRepository = new GenericRepository<Time>(context);
                }
                return timeRepository;
            }
        }

        public GenericRepository<Estadio> EstadioRepository
        {
            get
            {

                if (this.estadioRepository == null)
                {
                    this.estadioRepository = new GenericRepository<Estadio>(context);
                }
                return estadioRepository;
            }
        }

        public GenericRepository<Rodada> RodadaRepository
        {
            get
            {

                if (this.rodadaRepository == null)
                {
                    this.rodadaRepository = new GenericRepository<Rodada>(context);
                }
                return rodadaRepository;
            }
        }

        public ApostaRepository ApostaRepository
        {
            get
            {

                if (this.apostaRepository == null)
                {
                    this.apostaRepository = new ApostaRepository(context);
                }
                return apostaRepository;
            }
        }

        public RankingRepository RankingRepository
        {
            get
            {
                if (this.rankingRepository == null)
                {
                    this.rankingRepository = new RankingRepository(context);
                }
                return rankingRepository;
            }
        }

        public GenericRepository<UserProfile> UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new GenericRepository<UserProfile>(context);
                }
                return usuarioRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}