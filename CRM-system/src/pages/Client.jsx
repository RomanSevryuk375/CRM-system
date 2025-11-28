import { HashLink } from "react-router-hash-link"

import ClientBanner from "../components/ClientBanner/ClientBaner"
import ClientAdvantages from "../components/ClientAdvantages/ClientAdvantages"
import AdClient from "../components/AdClient/AdClient"
import ServicesClient from "../components/ServicesClient/ServicesClient"
import ClientFAQ from "../components/ClientFAQ/ClientFAQ"
import ClientContacts from "../components/ClientContacts/CliientContacts"
import ClientFooter from "../components/ClientFooter/ClientFooter"


function Client() {
  return (
    <>
      <ClientBanner />
      <div id='advantages'>
        <ClientAdvantages />
      </div>
      <AdClient />
      <div id='services'>
        <ServicesClient />
      </div>
      <div id='faq'>
        <ClientFAQ />
      </div>
      <div id='contacts'>
        <ClientContacts />
      </div>
      <ClientFooter />
    </>
  )
}

export default Client 