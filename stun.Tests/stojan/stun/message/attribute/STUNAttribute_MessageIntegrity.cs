using NUnit.Framework;
using STUN.Crypto;

namespace STUN.me.stojan.stun.message.attribute {
	/**
	 * Created by vuk on 03/12/16.
	 */
	[TestFixture]
	public class STUNAttribute_MessageIntegrityTest {
		[Test]
		public void Test() {
			byte[] reference = new byte[] {
				0x00, 0x01, 0x00, 0x2C, 0x21, 0x12, 0xA4, 0x42, 0x0A, 0x14, 0x1E, 0x28, 0x32, 0x3C, 0x46, 0x50,
				0x5A, 0x64, 0x6E, 0x78, 0x00, 0x06, 0x00, 0x03, 0x61, 0x3A, 0x62, 0x00, 0x00, 0x24, 0x00, 0x04,
				0x6E, 0x7F, 0x1E, 0xFF, 0x00, 0x25, 0x00, 0x00, 0x00, 0x08, 0x00, 0x14, 0xF5, 0xC6, 0x0F, 0x17,
				0xF5, 0xBB, 0xC0, 0x2D, 0xA6, 0xDE, 0x64, 0x4B, 0x36, 0xF8, 0xB6, 0xBE, 0x79, 0xA0, 0xA6, 0x16
			};



			HMAC_SHA1 hmacGenerator = null;

			byte[] bufferFromPool = new byte[1024];
			var msg = new STUNMessageBuilder(bufferFromPool);
			msg.SetMessageType(STUNClass.Request, STUNMethod.Binding);
			var tr = new Transaction(120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10);
			msg.SetTransaction(tr);
			msg.WriteAttribute(new STUNAttribute_Username("a:b"));
			msg.WriteAttribute(new STUNAttribute_Priority(0x6e7f1eff));
			msg.WriteAttribute(new STUNAttribute_UseCandidate());
			var stunReq = msg.Build("pass", false, ref hmacGenerator);


			CollectionAssert.AreEqual(reference, stunReq.ToArray());
		}
	}
}
