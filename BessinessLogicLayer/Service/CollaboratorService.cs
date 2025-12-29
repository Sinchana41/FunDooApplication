using BessinessLogicLayer.Interfaces;
using BessinessLogicLayer.Rabbitmq;
using DatabaseLogicLayer.Repository.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Service
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _repo;
        private readonly RabbitMqProducer _producer;

        public CollaboratorService(ICollaboratorRepository repo, RabbitMqProducer producer)
        {
            _repo = repo;
            _producer = producer;
        }

        public Collaborator Add(AddCollaboratorDto dto, int userId)
        {
            var collaborator = new Collaborator
            {
                Email = dto.Email,
                NoteId = dto.NoteId,
                UserId = userId
            };
            // 1️. Save to DB
            var result = _repo.Add(collaborator);

            // 2️.Publish event to RabbitMQ
            _producer.Publish("collaborator-added", new
            {
                NoteId = dto.NoteId,
                Email = dto.Email,
                UserId = userId
            });

            return result;
        }

        public IEnumerable<Collaborator> GetByNoteId(int noteId, int userId)
        {
            return _repo.GetByNoteId(noteId);
        }

        public void Delete(int collaboratorId, int userId)
        {
            var collaborator = _repo.GetById(collaboratorId);
            if (collaborator == null)
                throw new Exception("Collaborator not found");

            _repo.Delete(collaborator);
        }

        public void DeleteByEmail(int noteId, string email, int userId)
        {
            var collaborator = _repo.GetByNoteAndEmail(noteId, email);
            if (collaborator == null)
                throw new Exception("Collaborator not found");

            _repo.Delete(collaborator);
        }

        public IEnumerable<Note> GetSharedNotes(string email)
        {
            return _repo.GetSharedNotes(email);
        }
    }
}
