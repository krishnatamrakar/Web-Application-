ALTER TABLE ProfileAddress DROP CONSTRAINT [FK_ProfileAddress_Address];

ALTER TABLE ProfileAddress
ADD CONSTRAINT [FK_ProfileAddress_Address]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[Address]  ([AddressId])
    ON DELETE CASCADE;

ALTER TABLE ProfileAddress DROP CONSTRAINT [FK_ProfileAddress_Profile];

ALTER TABLE ProfileAddress
ADD CONSTRAINT [FK_ProfileAddress_Profile]
    FOREIGN KEY (ProfileId)
    REFERENCES [dbo].Profile  (ProfileId)
    ON DELETE CASCADE;

	ALTER TABLE ProfilePhone DROP CONSTRAINT [FK_ProfilePhone_Phone];

	Alter table ProfilePhone
	ADD CONSTRAINT [FK_ProfilePhone_Phone]
    FOREIGN KEY (PhoneId)
    REFERENCES [dbo].Phone  (PhoneId)
    ON DELETE CASCADE;


ALTER TABLE ProfilePhone DROP CONSTRAINT [FK_ProfilePhone_Profile];

	Alter table ProfilePhone
	ADD CONSTRAINT [FK_ProfilePhone_Profile]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profile]  ([ProfileId])
    ON DELETE CASCADE;



	CONSTRAINT [PK_ProfilePhone] PRIMARY KEY CLUSTERED ([ProfilePhoneId] ASC),
    CONSTRAINT [FK_ProfilePhone_Phone] FOREIGN KEY ([PhoneId]) REFERENCES [dbo].[Phone] ([PhoneId]),
    CONSTRAINT [FK_ProfilePhone_PhoneType] FOREIGN KEY ([PhoneTypeId]) REFERENCES [dbo].[PhoneType] ([PhoneTypeId]),
    CONSTRAINT [FK_ProfilePhone_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([ProfileId])