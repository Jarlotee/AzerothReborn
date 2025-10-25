//
// Copyright (C) 2013-2025 getMaNGOS <https://www.getmangos.eu>
//
// This program is free software. You can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation. either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY. Without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//

using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;

namespace Mangos.Extractor;

internal class Program
{
    private static int Main(string[] args)
    {
        var path = new Argument<DirectoryInfo>("path")
        {
            Description = "Path to your wow client exe",
            DefaultValueFactory = parseResult => new DirectoryInfo(Directory.GetCurrentDirectory()),
        };
        var root = new RootCommand();
        var updateFields = new Command("update-fields", "Extracts Update Fields");
        updateFields.SetAction(result =>
        {
            var wowPath = result.GetValue(path);
            Functions.ExtractUpdateFields();
            return 0;
        });
        var opCodes = new Command("op-codes", "Extracts Op Codes");
        opCodes.SetAction(result =>
        {
            var wowPath = result.GetValue(path);
            Functions.ExtractOpcodes();
            return 0;
        });
        var spellFailedReason = new Command("spell-failed-reason", "Extracts Spell Failed Reasons");
        spellFailedReason.SetAction(result =>
        {
            var wowPath = result.GetValue(path);
            Functions.ExtractSpellFailedReason();
            return 0;
        });
        var chatTypes = new Command("chat-types", "Extracts Chat Types");
        chatTypes.SetAction(result =>
        {
            var wowPath = result.GetValue(path);
            Functions.ExtractChatTypes();
            return 0;
        });
        var all = new Command("all", "Extracts Update Fields, Op Codes, Spell Failed Reasons, and Chat Types");
        all.SetAction(result =>
        {
            var wowPath = result.GetValue(path);
            Functions.ExtractUpdateFields();
            Functions.ExtractOpcodes();
            Functions.ExtractSpellFailedReason();
            Functions.ExtractChatTypes();
            return 0;
        });

        root.Subcommands.Add(updateFields);
        root.Subcommands.Add(opCodes);
        root.Subcommands.Add(spellFailedReason);
        root.Subcommands.Add(chatTypes);
        root.Subcommands.Add(all);
        root.Arguments.Add(path);

        var parseResult = root.Parse(args);

        if (parseResult.Errors.Any())
        {
            foreach (var parseError in parseResult.Errors)
            {
                Console.Error.WriteLine(parseError.Message);
            }

            return 1;
        }

        return parseResult.Invoke();
    }
}
