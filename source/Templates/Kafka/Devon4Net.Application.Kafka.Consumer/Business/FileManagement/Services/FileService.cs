﻿
using Devon4Net.Application.Kafka.Consumer.Domain.Database;
using Devon4Net.Application.Kafka.Consumer.Domain.RepositoryInterfaces;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Converters;
using Devon4Net.Infrastructure.UnitOfWork.Service;
using Devon4Net.Infrastructure.UnitOfWork.UnitOfWork;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Services
{

    public class FileService: Service<FileContext>, IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IUnitOfWork<FileContext> uoW) : base(uoW)
        {
            _fileRepository = uoW.Repository<IFileRepository>();
        }

        public DataPieceDto<byte[]> CreateFile(DataPieceDto<byte[]> dataPiece)
        {
            var result = _fileRepository.Create(DataPieceConverter.DtoToModel(dataPiece)).GetAwaiter().GetResult();
            return DataPieceConverter.ModelToDto(result);
        }
         
        public IList<string> GetDistinctFileGuids()
        {
            var result = _fileRepository.GetDistinctFileGuids();
            return result;
        }

        public IList<DataPieceDto<byte[]>> GetPiecesByFileGuid(string guid)
        {
            var result = _fileRepository.GetPiecesByFileGuid(guid);
            return DataPieceConverter.ModelToDto(result);
        }

        public bool IsFileComplete(string guid)
        {
            if (_fileRepository.IsFileComplete(guid)) return true;
            return false;
        }

        public bool DeleteFileByGuid(string guid)
        {
            return _fileRepository.DeleteByGuid(guid);
        }
    }
}
