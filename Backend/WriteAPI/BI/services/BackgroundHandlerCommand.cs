﻿using AutoMapper;
using BI.helpers;
using Domain.Commands.backgrounds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WriteContext.models;
using WriteContext.Repositories;

namespace BI.services
{
    public class BackgroundHandlerCommand: 
        IRequestHandler<DefaultBackground, Unit>,
        IRequestHandler<RemoveBackground, Unit>,
        IRequestHandler<UpdateBackground, Unit>,
        IRequestHandler<NewBackground, Unit>
    {
        private readonly UserRepository userRepository;
        private readonly BackgroundRepository backgroundRepository;
        private readonly PhotoHelpers photoHelpers;
        public BackgroundHandlerCommand(BackgroundRepository backgroundRepository,
                                        UserRepository userRepository,
                                        PhotoHelpers photoHelpers)
        {
            this.backgroundRepository = backgroundRepository;
            this.userRepository = userRepository;
            this.photoHelpers = photoHelpers;
        }

        public async Task<Unit> Handle(DefaultBackground request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            user.CurrentBackgroundId = null;
            await userRepository.Update(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoveBackground request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserWithBackgrounds(request.Email);
            var back = user.Backgrounds.Where(x => x.Id == request.Id).FirstOrDefault();
            if(back != null)
            {
                await backgroundRepository.DeleteBackground(back);
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateBackground request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            user.CurrentBackgroundId = request.Id;
            await userRepository.Update(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(NewBackground request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            var item = new Backgrounds()
            {
                Path = await photoHelpers.GetBase64(request.File),
                UserId = user.Id
            };
            await backgroundRepository.Add(item);
            return Unit.Value;
        }
    }
}
