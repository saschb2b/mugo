using System;
using CommandLine;
using CommandLine.Text;

namespace Mugo
{
	/// <summary>
	/// Command line options of the game.
	/// </summary>
	public class CommandLineOptions
	{
		[Option ('m', "noBackgroundMusic",
			DefaultValue = false,
  			HelpText = "Disables background music. It can still be enabled via the m hotkey.")]
		public bool NoBackgroundMusic { get; set; }

		[Option ('s', "seed",
	  		HelpText = "Sets the seed value of the random number generator.")]
		public int? Seed { get; set; }

		[HelpOption ('h', "help")]
		public string GetUsage ()
		{
			return HelpText.AutoBuild (this,
			  (HelpText current) => HelpText.DefaultParsingErrorsHandler (this, current));
		}
	}
}
