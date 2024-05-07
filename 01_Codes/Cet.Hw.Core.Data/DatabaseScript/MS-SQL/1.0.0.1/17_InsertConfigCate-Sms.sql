




INSERT INTO [Core].[ConfigurationCategory] ([Id], [Code], [Name], [IsAccessWrite])
	VALUES('c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'shortMessageService', 'Short Message Service', 1)
;
	
	
	
INSERT INTO [Core].[Configuration] ([Id], [ConfigCategoryId], [Code], [Name], [Description], [AllowClientAccess])
	values
	('e48f8f5d-59a7-43d1-8aaa-4fb302d91732', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPAppLogin', 'Login for https://www.thaibulksms.com', 'admin_otp', 0),
	('645dc3da-6a8a-4288-8158-8d533523e441', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPAppSecret', 'Password for https://www.thaibulksms.com', '9sJLU2uqzzfQHbX', 0),
	('5a102119-7eb7-4045-b5d3-1203e0f928c9', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPLimitTime', 'OTP limit time(minute)', '10', 0),
		
	('0bc8f2fb-c21b-4876-8652-5b990e4a0391', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPServiceURL', 'OTP Service URL', 'https://otp.thaibulksms.com', 0),
	('60f98568-3c62-4277-b51a-9e857eeb0a4e', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPRequestFormatPath', 'OTP request format path', '/v1/otp/request?key={0}&secret={1}&msisdn={2}', 0),
	('5c489260-1ed7-4262-b5aa-c846518f22d9', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPRequestParams', 'OTP request parameters [p01,p02,..,p(n)]', '1642580961097029,00c2d53f1408bacb2a183d1935ba6f31', 0),
	('3f43241f-c6a4-4a25-a28e-2392512c67bc', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPVerifierFormatPath', 'OTP verifier format path', '/v1/otp/verify?key={0}&secret={1}&token={2}&pin={3}', 0),
	('2e8ce41a-d4d8-4141-bb6f-44a68e0f461a', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPVerifierParams', 'OTP verifier parameters [p01,p02,..,p(n)]', '1642580961097029,00c2d53f1408bacb2a183d1935ba6f31', 0),
	
	('98553c1e-6933-4509-924b-190141fb2127', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'SmsServiceURL', 'Sms Service URL', 'http://www.thaibulksms.com/sms_api.php', 0),
	('bb846ab7-c98c-4f91-a47f-4629669f0536', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'SmsServiceNodePath', 'Sms service node path', '/SMS', 0),
	('93b90712-5a50-4286-bc2a-836c666fb690', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'SmsServiceSubNodePath', 'Sms service node path', '/SMS/QUEUE', 0),
	('7926e73f-67b8-44b3-a4ef-aa9d300052d4', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'InternalOTPLength', 'Custom length of one time password.', '4', 0),
	('95410550-c7c8-4623-a0e7-253cc154d170', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'InternalOTPLimitValue', 'Custom limit value of one time password.', '10000', 0),
	('a8da7a43-71ad-449e-87f4-1b532e980c65', 'c6946dfa-c1c7-4718-a62f-0b09b21883b4', 'OTPStandardFormat', 'Custom message layout for one time password.', 'MaMaBetting OTP : {0}. Expires in {1} minutes.', 0)
;
