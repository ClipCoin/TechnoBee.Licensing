1. Create certificate with private key (sha256):

	makecert -r -pe -n "CN=ElementStore Ltd" -ss CA -sr CurrentUser -a sha256 -cy authority -sky Exchange -sv Elementstore.pvk Elementstore.cer
	pvk2pfx /pvk Elementstore.pvk -pi ini9qwevbn -spc Elementstore.cer -pfx Elementstore.pfx

	P.S. makecert and pvk2pfx contains in windows sdk