using System;
using System.Xml;

namespace Lybrary.XML.Tests.FakeClasses.FakeXmls
{
    internal class FakeXmlFail
    {
        public string XString { get; set; }

        public FakeXmlFail()
        {
            this.XString =
                $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Catalog date=""{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}"" name=""First catalog"" number=""1"">
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Title>First book</Title>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>First patent</Title>
  </Element>
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Count>56</Count>
    <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <Number>10</Number>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>Second patent</Title>
  </Element>
  <Element type=""FakeBook"">
    <Authors>
      <Author>Author1</Author>
      <Author>Author2</Author>
    </Authors>
    <Count>51</Count>
    <Date>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</Date>
    <Title>Third book</Title>
  </Element>
  <Element type=""FakePatent"">
    <Inventors>
      <Inventor>Inventor1</Inventor>
      <Inventor>Inventor2</Inventor>
    </Inventors>
    <Number>10</Number>
    <AppDate>{XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local)}</AppDate>
    <Title>Third patent</Title>
  </Element>
</Catalog>";
        }
    }
}
